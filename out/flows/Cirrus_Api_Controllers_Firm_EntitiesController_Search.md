[web] GET /api/entities/  (Cirrus.Api.Controllers.Firm.EntitiesController.Search)  [L48–L64] status=200 [auth=user]
  └─ sends_request FindEntitiesQuery [L63]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindEntitiesQueryHandler.Handle [L70–L200]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L129]
          └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings [L15-L112]
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L89]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L91]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)

