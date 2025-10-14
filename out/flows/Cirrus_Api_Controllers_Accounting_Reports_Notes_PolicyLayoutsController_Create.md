[web] POST /api/accounting/reports/notes/policy-layouts/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyLayoutsController.Create)  [L62–L69] [auth=Authentication.AdminPolicy]
  └─ calls PolicyLayoutRepository.Add [L67]
  └─ writes_to PolicyLayout [L67]
    └─ reads_from PolicyLayouts
  └─ uses_service IControlledRepository<PolicyLayout>
    └─ method Add [L67]
  └─ sends_request OptimisePolicyLayoutModelCommand [L65]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Reports.PolicyLayouts.OptimisePolicyLayoutModelCommandHandler.Handle [L24–L94]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L37]

