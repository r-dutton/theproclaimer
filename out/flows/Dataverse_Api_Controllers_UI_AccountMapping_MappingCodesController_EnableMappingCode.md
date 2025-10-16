[web] PUT /api/ui/account-mapping/mapping-codes/{id:guid}/enable  (Dataverse.Api.Controllers.UI.AccountMapping.MappingCodesController.EnableMappingCode)  [L125–L132] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request EnableMappingCodeCommand -> EnableDisableMappingCodeCommandHandler [L129]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.MappingCodes.EnableDisableMappingCodeCommandHandler.Handle [L37–L89]
      └─ uses_service IControlledRepository<ExcludedMappingCode> (Scoped (inferred))
        └─ method ReadQuery [L72]
          └─ implementation Dataverse.Data.Repository.AccountMapping.ExcludedMappingCodeRepository.ReadQuery
      └─ uses_service IControlledRepository<MappingCode> (Scoped (inferred))
        └─ method ReadQuery [L67]
          └─ implementation Dataverse.Data.Repository.AccountMapping.MappingCodeRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ EnableMappingCodeCommand
    └─ handlers 1
      └─ EnableDisableMappingCodeCommandHandler

