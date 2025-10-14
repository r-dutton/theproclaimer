[web] PUT /api/accounting/sourcedata/sources/{id:Guid}  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.Update)  [L139–L145] [auth=user]
  └─ calls SourceRepository.WriteQuery [L142]
  └─ writes_to Source [L142]
  └─ uses_service IControlledRepository<Source>
    └─ method WriteQuery [L142]

