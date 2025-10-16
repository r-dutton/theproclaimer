[web] GET /api/one-platform/entities/{dataverseId}  (Cirrus.Api.Controllers.Dataverse.OnePlatformController.GetEntityId)  [L41–L51] status=200 [auth=Authentication.UserPolicy]
  └─ calls EntityRepository.ReadQuery [L44]
  └─ query Entity [L44]
  └─ uses_service IControlledRepository<Entity>
    └─ method ReadQuery [L44]
      └─ ... (no implementation details available)

