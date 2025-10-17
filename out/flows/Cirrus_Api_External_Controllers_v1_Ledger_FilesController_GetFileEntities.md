[web] GET /ledger/v1/files/{fileId:Guid}/entities  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.GetFileEntities)  [L95–L104] status=200
  └─ maps_to EntityReferenceDto [L98]
  └─ calls FileRepository.ReadQuery [L98]
  └─ query File [L98]
    └─ reads_from Files
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ File writes=0 reads=1
    └─ mappings 1
      └─ EntityReferenceDto

