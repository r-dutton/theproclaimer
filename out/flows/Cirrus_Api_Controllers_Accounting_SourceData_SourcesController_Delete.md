[web] DELETE /api/accounting/sourcedata/sources/{id:Guid}  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.Delete)  [L150–L156] [auth=user]
  └─ calls SourceRepository.Remove [L154]
  └─ calls SourceRepository.WriteQuery [L153]
  └─ writes_to Source [L154]
  └─ writes_to Source [L153]
  └─ uses_service IControlledRepository<Source>
    └─ method WriteQuery [L153]

