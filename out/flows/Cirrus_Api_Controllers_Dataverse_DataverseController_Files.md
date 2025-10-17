[web] POST /api/dataverse/files  (Cirrus.Api.Controllers.Dataverse.DataverseController.Files)  [L119–L126] status=201 [auth=Authentication.RequireTenantIdPolicy]
  └─ maps_to FileLightDto [L122]
    └─ automapper.registration CirrusMappingProfile (File->FileLightDto) [L192]
  └─ calls FileRepository.ReadQuery [L122]
  └─ query File [L122]
    └─ reads_from Files
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ File writes=0 reads=1
    └─ mappings 1
      └─ FileLightDto

