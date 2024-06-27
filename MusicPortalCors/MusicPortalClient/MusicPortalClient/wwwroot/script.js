$(function () {
    const urow = function (user) {
        return "<tr data-rowid='" + user.id + "'><td>" + user.id + "</td>" +
            "<td>" + user.login + "</td> <td>" + user.ufio + "</td>" +
            "<td>" + user.password + "</td>" +
            "<td><a class='btn second' id='ueditlink' data-id='" + user.id + "'>Изменить</a>" +
            "<a class='btn second' id='udellink' data-id='" + user.id + "'>Удалить</a></td></tr>";
    };
    const grow = function (genre) {
        return "<tr data-rowid='" + genre.id + "'><td>" + genre.id + "</td>" +
            "<td>" + genre.name + "</td>" +
            "<td><a class='btn second' id='geditlink' data-id='" + genre.id + "'>Изменить</a>" +
            "<a class='btn second' id='gdellink' data-id='" + genre.id + "'>Удалить</a></td></tr>";
    };
    const prow = function (performer) {
        return "<tr data-rowid='" + performer.id + "'><td>" + performer.id + "</td>" +
            "<td>" + performer.fio + "</td>" +
            "<td><a class='btn second' id='peditlink' data-id='" + performer.id + "'>Изменить</a> " +
            "<a class='btn second' id='pdellink' data-id='" + performer.id + "'>Удалить</a></td></tr>";
    };
    (function GetDatabase() {
        $.ajax({
            url: 'https://localhost:7134/api/Users',
            method: "GET",
            contentType: "application/json",
            success: function (users) {
                let rows = "";
                $.each(users, function (index, user) {
                    rows += urow(user);
                })
                $("#userbody").append(rows);
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        });
        $.ajax({
            url: 'https://localhost:7134/api/Genres',
            method: "GET",
            contentType: "application/json",
            success: function (genres) {
                let rows = "";
                $.each(genres, function (index, genre) {
                    rows += grow(genre);
                })
                $("#genrebody").append(rows);
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        });
        $.ajax({
            url: 'https://localhost:7134/api/Performers',
            method: "GET",
            contentType: "application/json",
            success: function (performers) {
                let rows = "";
                $.each(performers, function (index, performer) {
                    rows += prow(performer);
                })
                $("#performerbody").append(rows);
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        });
    })();
    function CreateUser(userLogin, userFio, userPassword) {
        $.ajax({
            url: "https://localhost:7134/api/Users",
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                login: userLogin,
                ufio: userFio,
                password: userPassword
            }),
            success: function (user) {
                $("#userbody").append(urow(user));
                document.forms["UserForm"].reset();
                document.forms["UserForm"].elements["Id"].value = 0;
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        })
    }
    function EditUser(userId, userLogin, userFio, userPassword) {
        let request = JSON.stringify({
            id: userId,
            login: userLogin,
            ufio: userFio,
            password: userPassword
        });
        $.ajax({
            url: "https://localhost:7134/api/Users",
            contentType: "application/json",
            method: "PUT",
            data: request,
            success: function (user) {
                $("tr[data-rowid='" + user.id + "']").replaceWith(urow(user));
                document.forms["UserForm"].reset();
                document.forms["UserForm"].elements["Id"].value = 0;
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        })
    }
    $("#submit1").click(function (e) {
        e.preventDefault();
        let id = document.forms["UserForm"].elements["Id"].value;
        let login = document.forms["UserForm"].elements["login"].value;
        let fio = document.forms["UserForm"].elements["ufio"].value;
        let password = document.forms["UserForm"].elements["password"].value;
        if (id == 0) CreateUser(login, fio, password);
        else EditUser(id, login, fio, password);
    });
    $("#reset1").click(function (e) {
        e.preventDefault();
        document.forms["UserForm"].reset();
        document.forms["UserForm"].elements["Id"].value = 0;
    });
    function CreateGenre(genreName) {
        $.ajax({
            url: "https://localhost:7134/api/Genres",
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                name: genreName
            }),
            success: function (genre) {
                $("#genrebody").append(grow(genre));
                document.forms["GenreForm"].reset();
                document.forms["GenreForm"].elements["Id"].value = 0;
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        })
    }
    function EditGenre(genreId, genreName) {
        let request = JSON.stringify({
            id: genreId,
            name: genreName
        });
        $.ajax({
            url: "https://localhost:7134/api/Genres",
            contentType: "application/json",
            method: "PUT",
            data: request,
            success: function (genre) {
                $("tr[data-rowid='" + genre.id + "']").replaceWith(grow(genre));
                document.forms["GenreForm"].reset();
                document.forms["GenreForm"].elements["Id"].value = 0;
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        })
    }
    $("#submit2").click(function (e) {
        e.preventDefault();
        let id = document.forms["GenreForm"].elements["Id"].value;
        let name = document.forms["GenreForm"].elements["name"].value;
        if (id == 0) CreateGenre(name);
        else EditGenre(id, name);
    });
    $("#reset2").click(function (e) {
        e.preventDefault();
        document.forms["GenreForm"].reset();
        document.forms["GenreForm"].elements["Id"].value = 0;
    });
    function CreatePerformer(performerFio) {
        $.ajax({
            url: "https://localhost:7134/api/Performers",
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                fio: performerFio,
            }),
            success: function (performer) {
                $("#performerbody").append(prow(performer));
                document.forms["PerformerForm"].reset();
                document.forms["PerformerForm"].elements["Id"].value = 0;
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        })
    }
    function EditPerformer(performerId, performerFio) {
        let request = JSON.stringify({
            id: performerId,
            fio: performerFio
        });
        $.ajax({
            url: "https://localhost:7134/api/Performers",
            contentType: "application/json",
            method: "PUT",
            data: request,
            success: function (performer) {
                $("tr[data-rowid='" + performer.id + "']").replaceWith(prow(performer));
                document.forms["PerformerForm"].reset();
                document.forms["PerformerForm"].elements["Id"].value = 0;
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        })
    }
    $("#submit3").click(function (e) {
        e.preventDefault();
        let id = document.forms["PerformerForm"].elements["Id"].value;
        let fio = document.forms["PerformerForm"].elements["fio"].value;
        if (id == 0) CreatePerformer(fio);
        else EditPerformer(id, fio);
    });
    $("#reset3").click(function (e) {
        e.preventDefault();
        document.forms["PerformerForm"].reset();
        document.forms["PerformerForm"].elements["Id"].value = 0;
    });
    function GetUser(id) {
        $.ajax({
            url: 'https://localhost:7134/api/Users/' + id,
            method: 'GET',
            contentType: "application/json",
            success: function (user) {
                document.forms["UserForm"].elements["Id"].value = user.id;
                document.forms["UserForm"].elements["login"].value = user.login;
                document.forms["UserForm"].elements["ufio"].value = user.ufio;
                document.forms["UserForm"].elements["password"].value = user.password;
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        });
    }
    $("body").on("click", "#ueditlink", function () {
        GetUser($(this).data("id"));
    });
    function DeleteUser(id) {
        if (!confirm("Вы действительно желаете удалить пользователя?")) return;
        $.ajax({
            url: "https://localhost:7134/api/Users/" + id,
            contentType: "application/json",
            method: "DELETE",
            success: function (user) {
                $("tr[data-rowid='" + user.id + "']").remove();
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        })
    }
    $("body").on("click", "#udellink", function () {
        DeleteUser($(this).data("id"));
    });
    function GetGenre(id) {
        $.ajax({
            url: 'https://localhost:7134/api/Genres/' + id,
            method: 'GET',
            contentType: "application/json",
            success: function (genre) {
                document.forms["GenreForm"].elements["Id"].value = genre.id;
                document.forms["GenreForm"].elements["name"].value = genre.name;
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        });
    }
    $("body").on("click", "#geditlink", function () {
        GetGenre($(this).data("id"));
    });
    function DeleteGenre(id) {
        if (!confirm("Вы действительно желаете удалить жанр?")) return;
        $.ajax({
            url: "https://localhost:7134/api/Genres/" + id,
            contentType: "application/json",
            method: "DELETE",
            success: function (genre) {
                $("tr[data-rowid='" + genre.id + "']").remove();
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        })
    }
    $("body").on("click", "#gdellink", function () {
        DeleteGenre($(this).data("id"));
    });
    function GetPerformer(id) {
        $.ajax({
            url: 'https://localhost:7134/api/Performers/' + id,
            method: 'GET',
            contentType: "application/json",
            success: function (performer) {
                document.forms["PerformerForm"].elements["Id"].value = performer.id;
                document.forms["PerformerForm"].elements["fio"].value = performer.fio;
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        });
    }
    $("body").on("click", "#peditlink", function () {
        GetPerformer($(this).data("id"));
    });
    function DeletePerformer(id) {
        if (!confirm("Вы действительно желаете удалить исполнителя?")) return;
        $.ajax({
            url: "https://localhost:7134/api/Performers/" + id,
            contentType: "application/json",
            method: "DELETE",
            success: function (performer) {
                $("tr[data-rowid='" + performer.id + "']").remove();
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.status + '\n' + exception);
            }
        })
    }
    $("body").on("click", "#pdellink", function () {
        DeletePerformer($(this).data("id"));
    });
});