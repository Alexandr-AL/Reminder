using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.Maui.Audio;
using Reminder.DAL.Entities;

namespace Reminder.Services
{
    public class EventProcessor
    {
        private readonly IEventsDataService _eventsDataService;
        private IDispatcherTimer _timer;
        private IAudioPlayer _audioPlayer;
        private IEnumerable<Event> _events;


        public EventProcessor(IEventsDataService eventsDataService)
        {
            _eventsDataService = eventsDataService;
            InitTimer();
            InitMedia();
        }

        private void InitTimer()
        {
            _timer = Application.Current.Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.IsRepeating = true;
            _timer.Tick += (s, e) => EventExecution();
        }

        private async void InitMedia()
        {
            using var mediaFile = await FileSystem.Current.OpenAppPackageFileAsync("Sound_21001.mp3");
            _audioPlayer = AudioManager.Current.CreatePlayer(mediaFile);
        }

        ~EventProcessor()
        {
            if (_timer.IsRunning)
                _timer.Stop();
        }

        private async void EventExecution()
        {
            if (_events is null) return;

            foreach (var @event in _events)
            {
                if (@event is null || !@event.Displayed) continue;

                if (CompareDateTimeEvent(@event))
                {
                    @event.Displayed = false;
                    await _eventsDataService.UpdateEventAsync(@event);
                    EventNotice(@event);
                }
            }
        }

        private async void EventNotice(Event @event)
        {
#if ANDROID
            if (!await LocalNotificationCenter.Current.AreNotificationsEnabled())
                await LocalNotificationCenter.Current.RequestNotificationPermission();

            var notification = new NotificationRequest
            {
                NotificationId = 101,
                Title = @event.Name,
                Description = @event.Description,
                CategoryType = NotificationCategoryType.Status,
                Android = new AndroidOptions()
                {
                    Priority = AndroidPriority.Max,
                    IconSmallName = new AndroidIcon(),
                    VisibilityType = AndroidVisibilityType.Public
                }
            };
            await LocalNotificationCenter.Current.Show(notification);
#endif

            await Toast.Make($"{@event.Name}\n{@event.Description}", ToastDuration.Long, 18).Show();

            _audioPlayer.Play();
        }

        private bool CompareDateTimeEvent(Event @event) => 
            @event.DateEvent.Date == DateTime.Now.Date &&
            @event.TimeEvent.Hours == DateTime.Now.Hour &&
            @event.TimeEvent.Minutes == DateTime.Now.Minute;

        public void Start(IEnumerable<Event> events)
        {
            if (events is null) return;

            _events = events;

            if (!_timer.IsRunning)
                _timer.Start();
        }
    }
}
