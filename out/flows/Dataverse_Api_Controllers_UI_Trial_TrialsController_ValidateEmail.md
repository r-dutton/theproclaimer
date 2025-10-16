[web] GET /api/ui/trials/valid-email  (Dataverse.Api.Controllers.UI.Trial.TrialsController.ValidateEmail)  [L32–L37] status=200 [AllowAnonymous]
  └─ sends_request ValidateTrialEmailQuery [L36]
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Trials.ValidateTrialEmailQueryHandler.Handle [L37–L134]
      └─ calls TenantRepository.ReadTable [L127]
      └─ calls TenantRepository.ReadTable [L112]
      └─ calls TenantRepository.WriteTable [L56]
      └─ uses_service List<string>
        └─ method Any [L118]
          └─ ... (no implementation details available)

