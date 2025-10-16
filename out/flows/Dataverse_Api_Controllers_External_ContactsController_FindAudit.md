[web] GET /audit/find  (Dataverse.Api.Controllers.External.ContactsController.FindAudit)  [L47–L51] status=200
  └─ calls ContactRepository.ReadQuery [L50]
  └─ query Contact [L50]
    └─ reads_from DVS_Clients
  └─ uses_service IControlledRepository<Contact>
    └─ method ReadQuery [L50]
      └─ ... (no implementation details available)

