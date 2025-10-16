[web] GET /api/commands/{id:guid}  (Workpapers.Next.API.Controllers.Templates.CommandsController.Get)  [L66–L73] status=200
  └─ maps_to CommandDto [L69]
    └─ automapper.registration WorkpapersMappingProfile (Command->CommandDto) [L360]
  └─ calls CommandRepository.ReadQuery [L69]
  └─ query Command [L69]
    └─ reads_from Commands
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Command writes=0 reads=1
    └─ mappings 1
      └─ CommandDto

