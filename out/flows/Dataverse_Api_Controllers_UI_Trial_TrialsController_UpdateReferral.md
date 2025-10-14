[web] POST /api/ui/trials/update-referral/{referralId:guid}  (Dataverse.Api.Controllers.UI.Trial.TrialsController.UpdateReferral)  [L71–L76] [AllowAnonymous]
  └─ sends_request UpdateUserReferralCommand [L75]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.UpdateUserReferralCommandHandler.Handle [L32–L51]
      └─ calls TenantRepository.WriteTable [L43]

