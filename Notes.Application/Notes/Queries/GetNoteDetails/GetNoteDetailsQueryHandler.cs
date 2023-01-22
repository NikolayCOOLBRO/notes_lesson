using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsQueryHandler
        : IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
    {
        private readonly INotesDbContext m_DbContext;
        private readonly IMapper m_Mapper;

        public GetNoteDetailsQueryHandler(INotesDbContext dbContext, IMapper mapper)
        {
            m_DbContext = dbContext;
            m_Mapper = mapper;
        }   

        public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await m_DbContext.Notes
                .FirstOrDefaultAsync(note =>
                    note.ID == request.Id, cancellationToken);

            if(entity == null || entity.ID != request.Id)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            return m_Mapper.Map<NoteDetailsVm>(entity);
        }
    }
}
