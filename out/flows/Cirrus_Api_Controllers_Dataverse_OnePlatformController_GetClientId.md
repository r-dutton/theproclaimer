[web] GET /api/one-platform/clients/{dataverseId}  (Cirrus.Api.Controllers.Dataverse.OnePlatformController.GetClientId)  [L30–L39] status=200 [auth=Authentication.UserPolicy]
  └─ calls ClientRepository.ReadQuery [L33]
  └─ query Client [L33]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Client writes=0 reads=1

