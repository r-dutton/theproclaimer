[web] PUT /api/ui/account-mapping/mapping-codes/for-client-group/{clientGroupId:guid}/{code}/mappings  (Dataverse.Api.Controllers.UI.AccountMapping.MappingCodesController.UpdateMappingCode)  [L171–L176] status=200 [auth=Authentication.UserPolicy,Authentication.UserPolicy]
  └─ sends_request CreateOrUpdateMappingCodeMappingsCommand [L175]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.MappingCodes.CreateOrUpdateMappingCodeMappingsCommandHandler.Handle [L39–L94]
      └─ uses_service UserService
        └─ method IsMaster [L54]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<ExternalReportingSystemMappingCode>
        └─ method WriteQuery [L56]
          └─ ... (no implementation details available)

