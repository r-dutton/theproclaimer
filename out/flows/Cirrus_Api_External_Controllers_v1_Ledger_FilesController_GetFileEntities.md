[web] GET /ledger/v1/files/{fileId:Guid}/entities  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.GetFileEntities)  [L95–L104]
  └─ maps_to EntityReferenceDto [L98]
  └─ calls FileRepository.ReadQuery [L98]
  └─ queries File [L98]
    └─ reads_from Files
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L98]

