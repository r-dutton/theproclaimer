[web] GET /audit  (Dataverse.Api.Controllers.External.ContactsController.GetAllAudits)  [L40–L45] status=200
  └─ calls ContactRepository.ReadQuery [L44]
  └─ query Contact [L44]
    └─ reads_from DVS_Clients
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Contact writes=0 reads=1

