[web] GET /api/entities/  (Cirrus.Api.Controllers.Firm.EntitiesController.Search)  [L48–L64] [auth=user]
  └─ sends_request FindEntitiesQuery [L63]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindEntitiesQueryHandler.Handle [L70–L200]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L129]
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L89]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L91]

