using EventManagementApp.Interfaces;
using EventManagementApp.Models;
using EventManagementApp.Exceptions;
using EventManagementApp.Repositories;
using static System.Reflection.Metadata.BlobBuilder;

namespace EventManagementApp.Services
{
    public class EventService : IEventService
    {
        private readonly IEventService _eventRepository;
        private readonly IRepository<int, Event> repository;
        public EventService(IRepository<int, Event> _repository)
        {
            repository = _repository;
        }

        public BookDTO AddBook(BookDTO bookDTO)
        {
            Books check;
            try
            {
                check = _booksRepository.GetAll().FirstOrDefault(u => u.Title == bookDTO.Title);
            }
            catch (Exception)
            {
                check = null;
            }
            if (check == null)
            {
                Books books = new Books()
                {
                    Title = bookDTO.Title,
                    Author = bookDTO.Author,
                    Genre = bookDTO.Genre,
                    PublishDate = bookDTO.PublishDate,
                    UserId = bookDTO.UserId,
                    Price = bookDTO.Price,
                    Image = "http://localhost:5103/Images/" + bookDTO.Image.FileName
                };
                var result = repository.Add(books);
                if (result != null)
                {
                    return bookDTO;
                }
                return null;
            }
            else
            {
                throw new AlreadyAvailableException();
            }
        }

        public Event Update(Event events)
        {
            var EventTitle = repository.GetAll().FirstOrDefault(e => e.Id == events.Id);
            if (EventTitle != null)
            {
                var result = repository.Update(events);
                if (result != null) return result;
            }
            return EventTitle;
        }
        public Event Remove(int Id)
        {
            var EventId = repository.GetAll().FirstOrDefault(e => e.Id == Id);
            if (EventId != null)
            {
                var result = repository.Delete(EventId.Id);
                if (result != null) return result;
            }
            return EventId;
        }


        public List<Event> GetEvents()
        {
            var events = repository.GetAll();
            if (events != null)
            {
                return events.ToList();
            }
            throw new NoEventsAvailableException();
        }

        public List<Event> GetByUserId(string UserId)
        {
            try
            {
                var events = repository.GetAll().Where(c => c.UserId == UserId).ToList();
                if (events != null)
                {
                    return events;
                }
            }
            catch (Exception)
            {
                throw new NoEventsAvailableException();
            }
            return null;
        }
        public List<Event> GetByEventId(int Id)
        {
            try
            {
                var events = repository.GetAll().Where(e => e.Id == Id).ToList();
                if (events != null)
                {
                    return events;
                }
            }
            catch (Exception)
            {
                throw new NoEventsAvailableException();
            }
            return null;
        }



    }
}