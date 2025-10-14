[web] POST /api/binder-status-requirements/  (Workpapers.Next.API.Controllers.Workpapers.BinderStatusRequirementsController.Create)  [L43–L50]
  └─ calls BinderStatusRequirementsRepository.Add [L47]
  └─ writes_to BinderStatusRequirements [L47]
    └─ reads_from BinderStatusRequirements
  └─ uses_service IControlledRepository<BinderStatusRequirements>
    └─ method Add [L47]

