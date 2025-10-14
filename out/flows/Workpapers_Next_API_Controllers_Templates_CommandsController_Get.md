[web] GET /api/commands/{id:guid}  (Workpapers.Next.API.Controllers.Templates.CommandsController.Get)  [L66–L73]
  └─ maps_to CommandDto [L69]
    └─ automapper.registration WorkpapersMappingProfile (Command->CommandDto) [L360]
  └─ calls CommandRepository.ReadQuery [L69]
  └─ queries Command [L69]
    └─ reads_from Commands
  └─ uses_service IControlledRepository<Command>
    └─ method ReadQuery [L69]

