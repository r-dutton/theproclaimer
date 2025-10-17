[web] POST /api/ui/trials/update-referral/{referralId:guid}  (Dataverse.Api.Controllers.UI.Trial.TrialsController.UpdateReferral)  [L71–L76] status=201 [AllowAnonymous]
  └─ sends_request UpdateUserReferralCommand -> UpdateUserReferralCommandHandler [L75]
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.UpdateUserReferralCommandHandler.Handle [L32–L51]
      └─ calls TenantRepository.WriteTable [L43]
  └─ impact_summary
    └─ requests 1
      └─ UpdateUserReferralCommand
    └─ handlers 1
      └─ UpdateUserReferralCommandHandler

