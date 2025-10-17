[web] GET /ledger/v1/files/  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.GetFilesAsync)  [L48–L59] status=200
  └─ calls FileRepository.ReadQuery [L53]
  └─ query File [L53]
    └─ reads_from Files
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ File writes=0 reads=1

