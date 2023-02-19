using Reminder.DAL.Entities;
using Reminder.Views;
using System.Collections.ObjectModel;

namespace Reminder.Controls
{
    public class EventsSearchHandler : SearchHandler
    {
        #region BindableProperty Events
        public static readonly BindableProperty EventsProperty = BindableProperty
            .Create(nameof(Events), 
                    typeof(IEnumerable<Event>), 
                    typeof(EventsSearchHandler));

        public IEnumerable<Event> Events 
        { 
            get => (IEnumerable<Event>)GetValue(EventsProperty); 
            set => SetValue(EventsProperty, value); 
        }
        #endregion

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (Events is null) return;

            ItemsSource = string.IsNullOrWhiteSpace(newValue)
                ? null
                : Events.Where(@event =>
                {
                    bool eventNameFound = false;
                    bool eventDescriptionFound = false;

                    if (@event.Name is not null)
                        eventNameFound = @event.Name.ToLower().Contains(newValue.ToLower());

                    if (@event.Description is not null)
                        eventDescriptionFound = @event.Description.ToLower().Contains(newValue.ToLower());

                    return eventNameFound || eventDescriptionFound;
                });
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            if (item is not null)
            {
                await Shell.Current.GoToAsync(nameof(CreateEditEventPage), 
                    new Dictionary<string, object>
                    {
                        { "Events", Events },
                        { "Event", item },
                        { "IsNew", false }
                    });
            }
        }
    }
}
