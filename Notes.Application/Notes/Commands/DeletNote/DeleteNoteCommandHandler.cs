using MediatR;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.DeletNote
{
    public class DeleteNoteCommandHandler :
        IRequestHandler<DeleteNoteCommand>
    {
        private readonly INotesDbContext m_DbContext;

        public DeleteNoteCommandHandler(INotesDbContext context) => m_DbContext = context;

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await
                m_DbContext.Notes
                    .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            m_DbContext.Notes.Remove(entity);
            await m_DbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
