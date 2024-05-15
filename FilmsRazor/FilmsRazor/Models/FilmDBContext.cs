using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FilmsRazor.Models
{
	public class FilmDBContext : DbContext
	{
		public DbSet<Film> Films { get; set; }
		public DbSet<Producer> Producers { get; set; }
		public DbSet<Genre> Genres { get; set; }

		public FilmDBContext(DbContextOptions<FilmDBContext> options) : base(options)
		{
			if (Database.EnsureCreated())
			{
				var producer = new Producer[]
				{
					new Producer() { Name = "Тодд Филлипс" },
					new Producer() { Name = "Фрэнсис Форд Коппола" },
					new Producer() { Name = "Дэвид Финчер" },
					new Producer() { Name = "Фрэнк Дарабонт" },
					new Producer() { Name = "Питер Джексон" },
					new Producer() { Name = "братья Вачовски" },
					new Producer() { Name = "Нил Бёргер" },
					new Producer() { Name = "Джеймс Ван"},
					new Producer() { Name = "Ирвин Кершнер"}
				};
				var genre = new Genre[]
				{
					new Genre() { Name="Психологический триллер"},
					new Genre(){ Name="Криминальная драма"},
					new Genre(){Name = "Драма"},
					new Genre(){Name = "Эпическое фэнтези"},
					new Genre(){Name = "Научная фантастика"},
					new Genre(){Name = "Мистика"},
					new Genre(){Name = "Фильм ужасов"},
					new Genre(){Name = "Боевик"}
				};
				var films = new Film[]
				{
					new Film(){Name = "Джокер", Producer = producer[0], Genre = genre[0], Year=2019, Poster="/img/joker.jpg", Description="Данный фильм повествует о возникновения анти героя по имени Джокер в Готэме во время 1980х годов. Вы увидите все подробности возникновения безумной личности и все жизненные факторы которые подтолкнули его на безумные деяния. Город получит его злую ухмылку и ярость с убийствами."},
					new Film(){Name = "Крестный отец", Producer = producer[1], Genre = genre[1], Year=1972, Poster="/img/TheGodfather.jpg", Description="Первая часть фильма \"Крестный отец\" является началом эпохальной криминальной саги, рассказывающей о сицилийской мафии, обосновавшейся в Нью-Йорке. Действия разворачиваются в 1945-1955 году. В центре сюжета оказывается мафиозная семья Корлеоне, под руководством гангстера старой закалки Вито."},
					new Film(){Name = "Бойцовский клуб", Producer = producer[2], Genre = genre[0], Year=1999, Poster="/img/FightClub.jpg", Description="События криминально-драматического триллера разворачиваются вокруг сотрудника страховой компании, который уже достаточно долгое время, как страдает хронической бессонницей."},
					new Film(){Name = "Побег из Шоушенка", Producer = producer[3], Genre = genre[2], Year=1994, Poster="/img/TheShawshankRedemption.jpg", Description="Действия фильма рассказывают о молодом человеке, по имени Энди. Он жил раньше обычной жизнью, не суливший беды. Но получается, что он оказывается в тюрьме."},
					new Film(){Name = "Властелин колец: Две крепости", Producer = producer[4], Genre = genre[3], Year=2002, Poster="/img/TheLordOfTheRings.jpg", Description="Фильм \"Властелин колец: Две крепости\" является продолжением похождений отважного хоббита Фродо, и его друга Сэма. Они прошли огонь и воду, сражались с превосходящими их по числу силами тьмы, и потеряли немало доблестных союзников на пути к горе Мордор, чтобы уничтожить кольцо Всевластия."},
					new Film(){Name = "Матрица", Producer = producer[5], Genre = genre[4], Year=1999, Poster="/img/Matrix.jpg", Description="В центре сюжета фантастического блокбастера «Матрица» находится обычный офисный клерк и по совместительству гениальный хакер Томас Андерсон известный в узких кругах как Нео. Его жизнь скучна и уныла, днем он выполняет обычную бумажную рутину, а ночью взламывает на заказ разные ресурсы за солидное вознаграждение."},
					new Film(){Name = "Дивергент", Producer = producer[6], Genre = genre[4], Year=2014, Poster="/img/Divergent.jpg", Description="Действия фильма «Дивергент» происходят в будущем, в городе Чикаго, где царит порядок благодаря строгим правилам, разделяющим жителей на разные касты. Каждый подросток, когда ему исполняется шестнадцать лет, должен выбрать одну фракцию из пяти доступных – Бесстрашных, Искренних, Отреченных, Эрудированных или Дружелюбных."},
					new Film(){Name = "Астрал", Producer = producer[7], Genre = genre[5], Year=2010, Poster="/img/Insidious.jpg", Description="В центре сюжета мистического триллера \"Астрал\" оказывается обычная американская семья, которая попала в серьезную передрягу. Все началось с того, когда супруги Джош и Рене вместе со своими детьми, переехали жить в новый дом."},
					new Film(){Name = "Проклятие Аннабель", Producer = producer[7], Genre = genre[6], Year=2014, Poster="/img/Annabelle.jpg", Description="Сюжет фильма ужасов \"Проклятие Аннабель\" об истории молодой семейной пары — Джон и Миа, которые счастливо живут в загородном доме и ждут своего первенца."},
					new Film(){Name = "Никогда не говори «никогда»", Producer = producer[8], Genre = genre[7], Year=1983, Poster="/img/NeverSayNeverAgain.jpg", Description="неофициальный кинофильм о Джеймсе Бонде, снятый Ирвином Кершнером. Вторая экранизация произведения Яна Флеминга «Операция „Шаровая молния“» (первая была в 1965 году)."},
				};
				Genres?.AddRange(genre);
				Producers?.AddRange(producer);
				Films?.AddRange(films);
				SaveChanges();
			}
		}
	}
}
