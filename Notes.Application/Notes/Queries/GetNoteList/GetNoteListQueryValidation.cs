﻿using FluentValidation;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryValidation : AbstractValidator<GetNoteListQuery>
    {
        public GetNoteListQueryValidation()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }
}
