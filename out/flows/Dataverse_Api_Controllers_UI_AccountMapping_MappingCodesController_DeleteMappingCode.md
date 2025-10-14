[web] DELETE /api/ui/account-mapping/mapping-codes/for-client-group/{clientGroupId:guid}/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.MappingCodesController.DeleteMappingCode)  [L198–L205] [auth=Authentication.UserPolicy,Authentication.UserPolicy]
  └─ sends_request DeleteMappingCodeCommand [L202]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.MappingCodes.DeleteMappingCodeCommandHandler.Handle [L34–L65]
      └─ uses_service IControlledRepository<MappingCode>
        └─ method WriteQuery [L48]
      └─ uses_service UserService
        └─ method IsMaster [L47]

