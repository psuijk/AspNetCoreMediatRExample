using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;

namespace RazorPagesLab.Pages.AddressBook
{
    public class UpdateAddressHandler
        : IRequestHandler<UpdateAddressRequest>
    {
        private readonly IRepo<AddressBookEntry> _repo;

        public UpdateAddressHandler(IRepo<AddressBookEntry> repo)
        {
            _repo = repo;
        }

        public Task Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
        {
           
            var specification = new EntryByIdSpecification(request.Id);
            var existingEntry = _repo.Find(specification).FirstOrDefault();
            existingEntry.Update(request.Line1, request.Line2, request.City, request.State, request.PostalCode);
            _repo.Update(existingEntry);
            return Task.CompletedTask;
        }
    }
}
