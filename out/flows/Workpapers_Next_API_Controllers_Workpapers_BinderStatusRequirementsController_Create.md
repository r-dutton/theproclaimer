[web] POST /api/binder-status-requirements/  (Workpapers.Next.API.Controllers.Workpapers.BinderStatusRequirementsController.Create)  [L43–L50] status=201
  └─ calls BinderStatusRequirementsRepository.Add [L47]
  └─ insert BinderStatusRequirements [L47]
    └─ reads_from BinderStatusRequirements
  └─ uses_service IControlledRepository<BinderStatusRequirements>
    └─ method Add [L47]
      └─ ... (no implementation details available)

