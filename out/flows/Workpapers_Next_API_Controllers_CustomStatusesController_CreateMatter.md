[web] POST /api/custom-statuses/matter  (Workpapers.Next.API.Controllers.CustomStatusesController.CreateMatter)  [L88–L102] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service IControlledRepository<MatterStatus> (AddScoped)
    └─ method ReadQuery [L92]
      └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 1
      └─ IControlledRepository<MatterStatus>

