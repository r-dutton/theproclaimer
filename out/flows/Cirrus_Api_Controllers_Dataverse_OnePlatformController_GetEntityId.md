[web] GET /api/one-platform/entities/{dataverseId}  (Cirrus.Api.Controllers.Dataverse.OnePlatformController.GetEntityId)  [L41–L51] [auth=Authentication.UserPolicy]
  └─ calls EntityRepository.ReadQuery [L44]
  └─ queries Entity [L44]
  └─ uses_service IControlledRepository<Entity>
    └─ method ReadQuery [L44]

