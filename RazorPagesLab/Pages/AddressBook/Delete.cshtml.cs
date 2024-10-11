using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;

namespace RazorPagesLab.Pages.AddressBook
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IRepo<AddressBookEntry> _repo;
        public DeleteModel(IRepo<AddressBookEntry> repo, IMediator mediator)
        {
            _repo = repo;
            _mediator = mediator;
        }

        public AddressBookEntry Entry { get; set; }

        public void OnGet(Guid id)
        {
            var specification = new EntryByIdSpecification(id);
            Entry = _repo.Find(specification).FirstOrDefault();
        }

        public ActionResult OnPost(Guid id)
        {
            var entryToDelete = _repo.Find(new EntryByIdSpecification(id)).FirstOrDefault();
            if (entryToDelete != null)
            {
                _repo.Remove(entryToDelete);
            }
            return RedirectToPage("Index");
        }
    }
}
