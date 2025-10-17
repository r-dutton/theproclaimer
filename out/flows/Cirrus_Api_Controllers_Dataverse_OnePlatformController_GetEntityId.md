[web] GET /api/one-platform/entities/{dataverseId}  (Cirrus.Api.Controllers.Dataverse.OnePlatformController.GetEntityId)  [L41–L51] status=200 [auth=Authentication.UserPolicy]
  └─ calls EntityRepository.ReadQuery [L44]
  └─ query Entity [L44]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Entity writes=0 reads=1

