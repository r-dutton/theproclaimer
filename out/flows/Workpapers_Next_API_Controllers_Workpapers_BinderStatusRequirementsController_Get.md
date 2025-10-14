[web] GET /api/binder-status-requirements/  (Workpapers.Next.API.Controllers.Workpapers.BinderStatusRequirementsController.Get)  [L33–L41]
  └─ maps_to BinderStatusRequirementsDto [L36]
    └─ automapper.registration WorkpapersMappingProfile (BinderStatusRequirements->BinderStatusRequirementsDto) [L911]
  └─ calls BinderStatusRequirementsRepository.ReadQuery [L36]
  └─ queries BinderStatusRequirements [L36]
    └─ reads_from BinderStatusRequirements
  └─ uses_service IControlledRepository<BinderStatusRequirements>
    └─ method ReadQuery [L36]

