[web] GET /ledger/v1/files/  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.GetFilesAsync)  [L48–L59]
  └─ calls FileRepository.ReadQuery [L53]
  └─ queries File [L53]
    └─ reads_from Files
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L53]

