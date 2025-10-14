[web] PUT /api/ui/account-mapping/mapping-codes/for-client-group/{clientGroupId:guid}/{code}/mappings  (Dataverse.Api.Controllers.UI.AccountMapping.MappingCodesController.UpdateMappingCode)  [L171–L176] [auth=Authentication.UserPolicy,Authentication.UserPolicy]
  └─ sends_request CreateOrUpdateMappingCodeMappingsCommand [L175]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.MappingCodes.CreateOrUpdateMappingCodeMappingsCommandHandler.Handle [L39–L94]
      └─ uses_service IControlledRepository<ExternalReportingSystemMappingCode>
        └─ method WriteQuery [L56]
      └─ uses_service UserService
        └─ method IsMaster [L54]

