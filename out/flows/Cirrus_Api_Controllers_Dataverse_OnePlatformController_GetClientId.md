[web] GET /api/one-platform/clients/{dataverseId}  (Cirrus.Api.Controllers.Dataverse.OnePlatformController.GetClientId)  [L30–L39] status=200 [auth=Authentication.UserPolicy]
  └─ calls ClientRepository.ReadQuery [L33]
  └─ query Client [L33]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L33]
      └─ ... (no implementation details available)

