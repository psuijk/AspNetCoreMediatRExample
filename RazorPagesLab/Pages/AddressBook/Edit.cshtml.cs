using System;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class EditModel : PageModel
{
	private readonly IMediator _mediator;
	private readonly IRepo<AddressBookEntry> _repo;

	public EditModel(IRepo<AddressBookEntry> repo, IMediator mediator)
	{
		_repo = repo;
		_mediator = mediator;
	}

	[BindProperty]
	public UpdateAddressRequest UpdateAddressRequest { get; set; }

	public void OnGet(Guid id)
	{
		UpdateAddressRequest = new UpdateAddressRequest();
		// Todo: Use repo to get address book entry, set UpdateAddressRequest fields.
		var specification = new EntryByIdSpecification(id);
		var entry = _repo.Find(specification).FirstOrDefault();

		if (entry != null)
		{
			UpdateAddressRequest.Id = entry.Id;
			UpdateAddressRequest.Line1 = entry.Line1;
			UpdateAddressRequest.Line2	= entry.Line2;
			UpdateAddressRequest.City = entry.City;
			UpdateAddressRequest.State = entry.State;
			UpdateAddressRequest.PostalCode = entry.PostalCode;
		}
		
	}

	public ActionResult OnPost()
	{
        // Todo: Use mediator to send a "command" to update the address book entry, redirect to entry list.
        if (ModelState.IsValid)
        {
            _mediator.Send(UpdateAddressRequest);
            return RedirectToPage("Index");
        }

        return Page();
	}
}