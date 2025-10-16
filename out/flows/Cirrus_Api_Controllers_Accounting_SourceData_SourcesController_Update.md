[web] PUT /api/accounting/sourcedata/sources/{id:Guid}  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.Update)  [L139–L145] status=200 [auth=user]
  └─ calls SourceRepository.WriteQuery [L142]
  └─ write Source [L142]
  └─ uses_service IControlledRepository<Source>
    └─ method WriteQuery [L142]
      └─ ... (no implementation details available)

