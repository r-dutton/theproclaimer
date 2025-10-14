[web] PUT /api/binder-status-requirements/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderStatusRequirementsController.Update)  [L52–L62]
  └─ calls BinderStatusRequirementsRepository.WriteQuery [L55]
  └─ writes_to BinderStatusRequirements [L55]
    └─ reads_from BinderStatusRequirements
  └─ uses_service IControlledRepository<BinderStatusRequirements>
    └─ method WriteQuery [L55]

