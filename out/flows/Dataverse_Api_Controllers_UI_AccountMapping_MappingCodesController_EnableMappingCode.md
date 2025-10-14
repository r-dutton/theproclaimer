[web] PUT /api/ui/account-mapping/mapping-codes/{id:guid}/enable  (Dataverse.Api.Controllers.UI.AccountMapping.MappingCodesController.EnableMappingCode)  [L125–L132] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request EnableMappingCodeCommand [L129]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.MappingCodes.EnableDisableMappingCodeCommandHandler.Handle [L37–L89]
      └─ uses_service IControlledRepository<ExcludedMappingCode>
        └─ method ReadQuery [L72]
      └─ uses_service IControlledRepository<MappingCode>
        └─ method ReadQuery [L67]

