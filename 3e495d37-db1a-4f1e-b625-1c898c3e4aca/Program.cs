using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    static async Task Main()
    {
        string token = "";
        var bot = new TelegramBotClient(token);

        using var cts = new CancellationTokenSource();

        bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            cancellationToken: cts.Token
        );

        Console.WriteLine("Бот запущен");
        Console.ReadLine();
    }

    static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is null) return;
        if (update.Message.Text is null) return;

        var text = update.Message.Text.ToLower();
        var userName = update.Message.From?.FirstName ?? "Участник";

        var projectDir = AppDomain.CurrentDomain.BaseDirectory;
        var rootDir = Path.GetFullPath(Path.Combine(projectDir, @"..\..\.."));

        if (text == "привет")
        {
            await bot.SendMessage(
                update.Message.Chat.Id,
                $"Здравствуй, {userName}! 🐾",
                cancellationToken: cancellationToken
            );
        }
        else if (text == "/start")
        {
            await bot.SendSticker(
                chatId: update.Message.Chat.Id,
                sticker: InputFile.FromFileId("CAACAgIAAxkBAAEPa7Bo0Yxgi0rVTAssMy_A60wSSQpzngAC2A8AAkjyYEsV-8TaeHRrmDYE"),
                cancellationToken: cancellationToken
            );

            var keyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "📘 Урок 1" },
                new KeyboardButton[] { "📷 Фото", "🎬 Видео" }
            })
            {
                ResizeKeyboard = true
            };

            await bot.SendMessage(
                update.Message.Chat.Id,
                $"Здравствуйте, {userName}! 🐾\n\n" +
                "Добро пожаловать на курс «Харизма и влияние от кота».\n\n" +
                "Нажмите кнопку ниже, чтобы начать.",
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
        }
        else if (text == "📘 урок 1")
        {
            var keyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "📘 Урок 2" }
            })
            {
                ResizeKeyboard = true
            };

            await bot.SendMessage(
                update.Message.Chat.Id,
                "📘 Урок 1\n\n" +
                "Наблюдение: Кошка всегда спокойна и не спешит. 😼\n\n" +
                "Приём: В жизни это означает — замедлиться и показывать уверенность.\n\n" +
                "Задание: Сегодня попробуйте говорить чуть медленнее обычного.",
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
        }
        else if (text == "📘 урок 2")
        {
            var keyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "📘 Урок 3" }
            })
            {
                ResizeKeyboard = true
            };

            await bot.SendMessage(
                update.Message.Chat.Id,
                "📘 Урок 2\n\n" +
                "Наблюдение: Кошка держит дистанцию и этим вызывает интерес. 🐈\n\n" +
                "Приём: Не заполняйте каждую паузу в разговоре.\n\n" +
                "Задание: Сделайте паузу перед ответом в диалоге.",
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
        }
        else if (text == "📘 урок 3")
        {
            var keyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "📘 Урок 4" }
            })
            {
                ResizeKeyboard = true
            };

            await bot.SendMessage(
                update.Message.Chat.Id,
                "📘 Урок 3\n\n" +
                "Наблюдение: Каждое движение кошки уверенное и осознанное. 🐾\n\n" +
                "Приём: Следите за осанкой и походкой.\n\n" +
                "Задание: Сегодня ходите спокойно и прямо.",
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
        }
        else if (text == "📘 урок 4")
        {
            var keyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "🎓 Завершить курс" }
            })
            {
                ResizeKeyboard = true
            };

            await bot.SendMessage(
                update.Message.Chat.Id,
                "📘 Урок 4\n\n" +
                "Наблюдение: Кошка редко требует, но часто получает. 🐱\n\n" +
                "Приём: Не выпрашивайте, а создавайте ощущение, что выбор за вами.\n\n" +
                "Задание: В разговоре попробуйте говорить так, будто именно вас хотят убедить.",
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
        }
        else if (text == "🎓 завершить курс")
        {
            var keyboard = new ReplyKeyboardRemove();

            await bot.SendMessage(
                update.Message.Chat.Id,
                $"Поздравляю, {userName}! 🎓\n\n" +
                "Вы завершили курс «Харизма и влияние от кота».\n\n" +
                "============================\n" +
                "СЕРТИФИКАТ О ЗАВЕРШЕНИИ КУРСА\n\n" +
                $"Выдан: {userName}\n" +
                "Курс: «Харизма и влияние от кота»\n" +
                "Статус: успешно завершён\n" +
                "Дата: " + DateTime.Now.ToString("dd.MM.yyyy") + "\n" +
                "============================\n\n" +
                "Спасибо за участие. Теперь ваша задача — применять эти приёмы в жизни. 🐾",
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
        }
        else if (text == "📷 фото")
        {
            var photoPath = Path.Combine(rootDir, "Media", "Photos", "cat_photo.jpg");
            if (File.Exists(photoPath))
            {
                await using var stream = File.OpenRead(photoPath);
                await bot.SendPhoto(
                    chatId: update.Message.Chat.Id,
                    photo: InputFile.FromStream(stream, "cat_photo.jpg"),
                    cancellationToken: cancellationToken
                );
            }
            else
            {
                await bot.SendMessage(update.Message.Chat.Id, "Фото не найдено 😿", cancellationToken: cancellationToken);
            }
        }
        else if (text == "🎬 видео")
        {
            var videoPath = Path.Combine(rootDir, "Media", "Videos", "cat_video.mp4");
            if (File.Exists(videoPath))
            {
                await using var stream = File.OpenRead(videoPath);
                await bot.SendVideo(
                    chatId: update.Message.Chat.Id,
                    video: InputFile.FromStream(stream, "cat_video.mp4"),
                    cancellationToken: cancellationToken
                );
            }
            else
            {
                await bot.SendMessage(update.Message.Chat.Id, "Видео не найдено 😿", cancellationToken: cancellationToken);
            }
        }
    }

    static Task HandleErrorAsync(ITelegramBotClient bot, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(exception.Message);
        return Task.CompletedTask;
    }
}