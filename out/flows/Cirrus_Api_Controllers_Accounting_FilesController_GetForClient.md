[web] GET /api/accounting/files/for-client/{clientId}  (Cirrus.Api.Controllers.Accounting.FilesController.GetForClient)  [L103–L128] status=200 [auth=user]
  └─ maps_to FileLightDto [L122]
    └─ automapper.registration CirrusMappingProfile (File->FileLightDto) [L192]
  └─ calls FileRepository.ReadQuery [L122]
  └─ calls ClientRepository.ReadQuery [L112]
  └─ query File [L122]
    └─ reads_from Files
  └─ query Client [L112]
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ Client writes=0 reads=1
      └─ File writes=0 reads=1
    └─ mappings 1
      └─ FileLightDto

