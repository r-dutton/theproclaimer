[web] DELETE /api/accounting/sourcedata/sources/{id:Guid}  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.Delete)  [L150–L156] status=200 [auth=user]
  └─ calls SourceRepository.Remove [L154]
  └─ calls SourceRepository.WriteQuery [L153]
  └─ delete Source [L154]
  └─ write Source [L153]
  └─ uses_service IControlledRepository<Source>
    └─ method WriteQuery [L153]
      └─ ... (no implementation details available)

