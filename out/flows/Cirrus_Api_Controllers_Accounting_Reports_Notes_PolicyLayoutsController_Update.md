[web] PUT /api/accounting/reports/notes/policy-layouts/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyLayoutsController.Update)  [L74–L80] status=200 [auth=Authentication.AdminPolicy]
  └─ calls PolicyLayoutRepository.WriteQuery [L78]
  └─ write PolicyLayout [L78]
    └─ reads_from PolicyLayouts
  └─ uses_service IControlledRepository<PolicyLayout>
    └─ method WriteQuery [L78]
      └─ ... (no implementation details available)
  └─ sends_request OptimisePolicyLayoutModelCommand [L77]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Reports.PolicyLayouts.OptimisePolicyLayoutModelCommandHandler.Handle [L24–L94]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L37]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

