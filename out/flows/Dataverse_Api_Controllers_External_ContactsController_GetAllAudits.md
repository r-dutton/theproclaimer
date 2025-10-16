[web] GET /audit  (Dataverse.Api.Controllers.External.ContactsController.GetAllAudits)  [L40–L45] status=200
  └─ calls ContactRepository.ReadQuery [L44]
  └─ query Contact [L44]
    └─ reads_from DVS_Clients
  └─ uses_service IControlledRepository<Contact>
    └─ method ReadQuery [L44]
      └─ ... (no implementation details available)

