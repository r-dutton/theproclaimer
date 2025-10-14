[web] POST /api/dataverse/files  (Cirrus.Api.Controllers.Dataverse.DataverseController.Files)  [L119–L126] [auth=Authentication.RequireTenantIdPolicy]
  └─ maps_to FileLightDto [L122]
    └─ automapper.registration CirrusMappingProfile (File->FileLightDto) [L192]
  └─ calls FileRepository.ReadQuery [L122]
  └─ queries File [L122]
    └─ reads_from Files
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L122]

