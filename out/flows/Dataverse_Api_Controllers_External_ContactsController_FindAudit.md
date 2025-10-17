[web] GET /audit/find  (Dataverse.Api.Controllers.External.ContactsController.FindAudit)  [L47–L51] status=200
  └─ calls ContactRepository.ReadQuery [L50]
  └─ query Contact [L50]
    └─ reads_from DVS_Clients
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Contact writes=0 reads=1

