using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler
        : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INotesDbContext m_DbContex;

        public CreateNoteCommandHandler(INotesDbContext dbContext) => 
            m_DbContex = dbContext;

        public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = new Note
            {
                UserId = request.UserId,
                Title = request.Title,
                Details = request.Details,
                ID = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditTime = null
            };

            await m_DbContex.Notes.AddAsync(note, cancellationToken);
            await m_DbContex.SaveChangesAsync(cancellationToken);

            return note.ID;
        }
    }
}
