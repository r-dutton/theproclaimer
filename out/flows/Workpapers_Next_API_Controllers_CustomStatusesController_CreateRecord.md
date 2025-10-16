[web] POST /api/custom-statuses/record  (Workpapers.Next.API.Controllers.CustomStatusesController.CreateRecord)  [L72–L86] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service IControlledRepository<RecordStatus> (AddScoped)
    └─ method ReadQuery [L76]
      └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 1
      └─ IControlledRepository<RecordStatus>

