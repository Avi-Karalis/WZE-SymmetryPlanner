using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Data
{
    public static class SeedData
    {
        public static void Seed(ApplicationDbContext context) {

            // Check if any data already exists to avoid seeding again
            if (context.Units.Any() || context.Weapons.Any() || context.WeaponSpecialAbilities.Any() || context.UnitSpecialAbilities.Any()) {
                return; 
            }
            WeaponSpecialAbility accurate1 = new() { Name = "Accurate", ValueX = "1", Description = "Shoot Actions performed with this Weapon score Critical Successes on a natural roll of {X} or lower, instead of only on a natural roll of “1.”" };
            WeaponSpecialAbility accurate2 = new() { Name = "Accurate", ValueX = "2", Description = "Shoot Actions performed with this Weapon score Critical Successes on a natural roll of {X} or lower, instead of only on a natural roll of “1.”" };
            WeaponSpecialAbility accurate3 = new() { Name = "Accurate", ValueX = "3", Description = "Shoot Actions performed with this Weapon score Critical Successes on a natural roll of {X} or lower, instead of only on a natural roll of “1.”" };
            WeaponSpecialAbility accurate4 = new() { Name = "Accurate", ValueX = "4", Description = "Shoot Actions performed with this Weapon score Critical Successes on a natural roll of {X} or lower, instead of only on a natural roll of “1.”" };
            WeaponSpecialAbility burst1 = new() { Name = "Burst", ValueX = "1", Description = "Shoot Actions with this Weapon may perform up to {X} Shoot Action Tests assigned to any combination of Targets within a four inch diameter area." };
            WeaponSpecialAbility burst2 = new() { Name = "Burst", ValueX = "2", Description = "Shoot Actions with this Weapon may perform up to {X} Shoot Action Tests assigned to any combination of Targets within a four inch diameter area." };
            WeaponSpecialAbility burst3 = new() { Name = "Burst", ValueX = "3", Description = "Shoot Actions with this Weapon may perform up to {X} Shoot Action Tests assigned to any combination of Targets within a four inch diameter area." };
            WeaponSpecialAbility burst4 = new() { Name = "Burst", ValueX = "4", Description = "Shoot Actions with this Weapon may perform up to {X} Shoot Action Tests assigned to any combination of Targets within a four inch diameter area." };
            WeaponSpecialAbility cloud1 = new() { Name = "Cloud", ValueX = "1", Description = "This Weapon is subject to the Cloud Area of Effect Weapon rules and has a Threat Zone {X} inches in radius." };
            WeaponSpecialAbility cloud2 = new() { Name = "Cloud", ValueX = "2", Description = "This Weapon is subject to the Cloud Area of Effect Weapon rules and has a Threat Zone {X} inches in radius." };
            WeaponSpecialAbility cloud3 = new() { Name = "Cloud", ValueX = "3", Description = "This Weapon is subject to the Cloud Area of Effect Weapon rules and has a Threat Zone {X} inches in radius." };
            WeaponSpecialAbility concussive = new() { Name = "Concussive", Description = "All Units in the Threat Zone created when this weapon performs a Shoot Action receive a Pinned Counter after the action, regardless of the Success of the Shoot Action." };
            WeaponSpecialAbility continuous = new() { Name = "Continuous", Description = "If a Unit Fails an Armor Test against this Weapon, it immediately takes another hit nusing the Weapon’s DAM Rating (ignoring Aim or other bonuses). A Failed Armor Test against this second hit will not trigger the Continuous Weapon Special Ability." };
            WeaponSpecialAbility divine = new() { Name = "Explosive", ValueX = "1", Description = "This Weapon is subject to the Explosive Area of Effect Weapon rules and has a nThreat Zone {X} inches in radius." };
            WeaponSpecialAbility explosive1 = new() { Name = "Explosive", ValueX = "1", Description = "This Weapon is subject to the Explosive Area of Effect Weapon rules and has a Threat Zone {X} inches in radius." };
            WeaponSpecialAbility explosive2 = new() { Name = "Explosive", ValueX = "2", Description = "This Weapon is subject to the Explosive Area of Effect Weapon rules and has a Threat Zone {X} inches in radius." };
            WeaponSpecialAbility explosive3 = new() { Name = "Explosive", ValueX = "3", Description = "This Weapon is subject to the Explosive Area of Effect Weapon rules and has a Threat Zone {X} inches in radius." };
            WeaponSpecialAbility gruesome3 = new() { Name = "Gruesome", ValueX = "3", Description = "When an Enemy Unit is removed as a Casualty due to a Shoot or Strike Action Test performed with this Weapon, all Enemy Units within {X} inches of the Casualty must Succeed at a TN (LD) Test or receive a Pinned Counter." };
            WeaponSpecialAbility indirect = new() { Name = "Indirect", Description = "This Weapon may perform Indirect Fire Shoot Actions." };
            WeaponSpecialAbility multiStrike1 = new() { Name = "Multi-strike", ValueX = "1", Description = "For each Strike Action with this Weapon, a Strike Action Test may be assigned nto up to {X} Enemy Units." };
            WeaponSpecialAbility multiStrike2 = new() { Name = "Multi-strike", ValueX = "2", Description = "For each Strike Action with this Weapon, a Strike Action Test may be assigned nto up to {X} Enemy Units." };
            WeaponSpecialAbility multiStrike3 = new() { Name = "Multi-strike", ValueX = "3", Description = "For each Strike Action with this Weapon, a Strike Action Test may be assigned nto up to {X} Enemy Units." };
            WeaponSpecialAbility projected = new() { Name = "Projected", Description = "This Weapon is subject to the Projected Area of Effect Weapon rules." };
            WeaponSpecialAbility reach1 = new() { Name = "Reach", ValueX = "1", Description = "Units armed with this Weapon may perform Strike Actions against Enemy Units in Clear or Obstructed LOS up to {X} inches away, including as part of a Charge Action, even though nthe Units are not Engaged." };
            WeaponSpecialAbility reach2 = new() { Name = "Reach", ValueX = "2", Description = "Units armed with this Weapon may perform Strike Actions against Enemy Units in Clear or Obstructed LOS up to {X} inches away, including as part of a Charge Action, even though nthe Units are not Engaged." };
            WeaponSpecialAbility reach3 = new() { Name = "Reach", ValueX = "3", Description = "Units armed with this Weapon may perform Strike Actions against Enemy Units in Clear or Obstructed LOS up to {X} inches away, including as part of a Charge Action, even though nthe Units are not Engaged." };
            WeaponSpecialAbility recoil1 = new() { Name = "Recoil", ValueX = "1", Description = "The TN for any Shoot or Strike Actions performed with this Weapon suffer a -{X} penalty." };
            WeaponSpecialAbility recoil2 = new() { Name = "Recoil", ValueX = "2", Description = "The TN for any Shoot or Strike Actions performed with this Weapon suffer a -{X} penalty." };
            WeaponSpecialAbility recoil3 = new() { Name = "Recoil", ValueX = "3", Description = "The TN for any Shoot or Strike Actions performed with this Weapon suffer a -{X} penalty." };
            WeaponSpecialAbility recoil4 = new() { Name = "Recoil", ValueX = "4", Description = "The TN for any Shoot or Strike Actions performed with this Weapon suffer a -{X} penalty." };
            WeaponSpecialAbility silenced = new() { Name = "Silenced", Description = "Shoot Actions performed with this Weapon do not trigger Counterattack Reactions from any Enemy Unit who has Obstructed LOS to this model." };
            WeaponSpecialAbility smoke = new() { Name = "Smoke", Description = "Any LOS Lines drawn through any part of this Weapon’s Area of Effect are Blocked. The Area of Effect is considered to be of infinite height." };
            WeaponSpecialAbility suppressive = new() { Name = "Suppressive", Description = "If all of the Shoot or Strike Tests from a Shoot or Strike Action using this Weapon target the same Enemy Unit (this must be a Primary Target for Area of Effect Weapons), that Enemy Unit receives a Pinned Counter after the action, regardless of the Success of the Shoot nor Strike Action." };
            WeaponSpecialAbility symmetry = new() { Name = "Symmetry", Description = "Shoot and Strike Action Tests with this Weapon are TN (PW) Tests, instead of TN (MW) or TN (CC) as normal." };
            WeaponSpecialAbility terrifying = new() { Name = "Terrifying", Description = "Armor Tests performed against this Weapon’s Shoot and Strike Actions are TN (LD) Tests, instead of TN (AR) as normal." };
            WeaponSpecialAbility thrust1 = new() { Name = "Thrust", ValueX = "1", Description = "When performing a Strike Action as part of a Charge Action, increase the DAM of this Weapon by {X}." };
            WeaponSpecialAbility thrust2 = new() { Name = "Thrust", ValueX = "2", Description = "When performing a Strike Action as part of a Charge Action, increase the DAM of this Weapon by {X}." };
            WeaponSpecialAbility thrust3 = new() { Name = "Thrust", ValueX = "3", Description = "When performing a Strike Action as part of a Charge Action, increase the DAM of this Weapon by {X}." };
            WeaponSpecialAbility thrust4 = new() { Name = "Thrust", ValueX = "4", Description = "When performing a Strike Action as part of a Charge Action, increase the DAM of this Weapon by {X}." };
            WeaponSpecialAbility thrust5 = new() { Name = "Thrust", ValueX = "5", Description = "When performing a Strike Action as part of a Charge Action, increase the DAM of this Weapon by {X}." };
            WeaponSpecialAbility vicious = new() { Name = "Vicious", Description = "This Weapon’s DAM increases by +2 per Wound Counter on an Enemy Unit targeted by a Strike or Shoot Action using this Weapon. Strike Actions additionally receive +2 to the Unit’s CC Rating per Wound Counter." };
            WeaponSpecialAbility flurry2 = new() { Name = "Flurry", ValueX = "2", Description = "Strike Actions with this Weapon may perform up to {X} Strike Action Tests assigned to any combination of Engaged Targets." };
            List<WeaponSpecialAbility> weaponSpecialAbilities = [flurry2, gruesome3, accurate1, accurate2, accurate3, accurate4, burst1, burst2, burst3, burst4, cloud1, cloud2, cloud3, concussive, continuous, divine, explosive1, explosive2, explosive3, indirect, multiStrike1, multiStrike3, multiStrike2, projected, reach1, reach2, reach3, recoil1, recoil2, recoil3, recoil4, silenced, smoke, suppressive, symmetry, terrifying, thrust1, thrust2, thrust3, thrust4, thrust5, vicious];
            context.WeaponSpecialAbilities.AddRange(weaponSpecialAbilities);
            context.SaveChanges();

            UnitSpecialAbility aggressive = new() { Name = "Aggresive", Description = "After this Unit completes a Shoot Action it may be moved 2” toward any Enemy Unit to which it has non-Blocked LOS." };
            UnitSpecialAbility art2 = new() { Name = "Art", ValueX = "2", Description = "This Unit chooses {X} different Art Disciplines from its available choices during Game Setup." };
            UnitSpecialAbility art4 = new() { Name = "Art", ValueX = "4", Description = "This Unit chooses {X} different Art Disciplines from its available choices during Game Setup." };
            UnitSpecialAbility awareness = new() { Name = "Awareness", Description = "While possessing no more than one Reaction Counter, this Unit may be assigned a Reaction Counter." };
            UnitSpecialAbility blitz = new() { Name = "Blitz", Description = "When this Unit completes a Shoot Action that resulted in an Armor Test Failure for at least one Enemy Unit, this Unit may immediately perform a free Move Action." };
            UnitSpecialAbility blur2 = new() { Name = "Blur", ValueX = "2", Description = "This Unit receives a bonus of {X} to its DEF Characteristic against Shoot Action Tests targeting it as part of any Reaction." };
            UnitSpecialAbility blur3 = new() { Name = "Blur", ValueX = "3", Description = "This Unit receives a bonus of {X} to its DEF Characteristic against Shoot Action Tests targeting it as part of any Reaction." };
            UnitSpecialAbility camaraderie = new() { Name = "Camaraderie", Description = "When this Unit is removed as a Casualty, all other Friendly Units with the Camaraderie Unit Special Ability within 12 inches and non-Blocked LOS remove their Pinned and Reaction Counters." };
            UnitSpecialAbility camouflage1 = new() { Name = "Camouflage", Description = "Any Obstruction modifiers to the TN for a Shoot Action Test targeting this Unit are increased by +{X}." };
            UnitSpecialAbility camouflage2 = new() { Name = "Camouflage", Description = "Any Obstruction modifiers to the TN for a Shoot Action Test targeting this Unit are increased by +{X}." };
            UnitSpecialAbility commandVR = new() { Name = "Command", ValueX = "Venusian Ranger", ValueY = "1", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandBL = new() { Name = "Command", ValueX = "Blitzer", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandEM = new() { Name = "Command", ValueX = "Etoiles Mortant", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandHS = new() { Name = "Command", ValueX = "Hussar", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandFM = new() { Name = "Command", ValueX = "Free Marine", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandSS = new() { Name = "Command", ValueX = "Sunset Striker", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandMB = new() { Name = "Command", ValueX = "Martian Banshee", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandFB = new() { Name = "Command", ValueX = "Freedom Brigade", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandCS = new() { Name = "Command", ValueX = "Chasseur", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandMM = new() { Name = "Command", ValueX = "Mirrorman", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandBB = new() { Name = "Command", ValueX = "Blood Beret", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandGL = new() { Name = "Command", ValueX = "Golden Lion", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandTR = new() { Name = "Command", ValueX = "Trencher", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandWC = new() { Name = "Command", ValueX = "Wolfbane Commando", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandHM = new() { Name = "Command", ValueX = "Hatamoto", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandSB = new() { Name = "Command", ValueX = "Shinobi", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandSM = new() { Name = "Command", ValueX = "Samurai", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandMT = new() { Name = "Command", ValueX = "Mortificator", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandAB = new() { Name = "Command", ValueX = "Any Brotherhood", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandUL = new() { Name = "Command", ValueX = "Undead Legionnaire", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandNM = new() { Name = "Command", ValueX = "Necromutant", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandULNM1 = new() { Name = "Command", ValueX = "Undead Legionnaire, Necromutant", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility commandAA = new() { Name = "Command", ValueX = "Any Algeroth", Description = "Once per Turn, if this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to perform a TN (LD) Test. On a Success, a Friendly Unit of Unit Type {X} within 12” and non-Blocked LOS may perform one Action when this Unit completes its Activation." };
            UnitSpecialAbility controller3 = new() { Name = "Controller", ValueX = "3", Description = "Friendly Units with the Zombie Unit Special Ability within {X} inches of this Unit are not restricted in the Actions they may perform and may Reposition when Activated." };
            UnitSpecialAbility controller6 = new() { Name = "Controller", ValueX = "6", Description = "Friendly Units with the Zombie Unit Special Ability within {X} inches of this Unit are not restricted in the Actions they may perform and may Reposition when Activated." };
            UnitSpecialAbility controller9 = new() { Name = "Controller", ValueX = "9", Description = "Friendly Units with the Zombie Unit Special Ability within {X} inches of this Unit are not restricted in the Actions they may perform and may Reposition when Activated." };
            UnitSpecialAbility controller12 = new() { Name = "Controller", ValueX = "12", Description = "Friendly Units with the Zombie Unit Special Ability within {X} inches of this Unit are not restricted in the Actions they may perform and may Reposition when Activated." };
            UnitSpecialAbility determination = new() { Name = "Determination", Description = "This Unit may re-roll any TN (PW) or TN (LD) Tests Failures." };
            UnitSpecialAbility dodge = new() { Name = "Dodge", Description = "When this Unit performs a Dive for Cover Reaction, perform a Test with a TN equal to the lowest die result of the Enemy Unit’s Shoot Action. On a Success, this Unit does not receive a Reaction Counter." };
            UnitSpecialAbility driven = new() { Name = "Driven", Description = "The Suppressive Weapon Special Ability is ignored when targeting this Unit." };
            UnitSpecialAbility duelist1 = new() { Name = "Duelist", ValueX = "1", Description = "This Unit modifies the Rating of its DEF Characteristic by {X} when targeted by an Enemy Unit’s Strike Action." };
            UnitSpecialAbility duelist2 = new() { Name = "Duelist", ValueX = "2", Description = "This Unit modifies the Rating of its DEF Characteristic by {X} when targeted by an Enemy Unit’s Strike Action." };
            UnitSpecialAbility duelist3 = new() { Name = "Duelist", ValueX = "3", Description = "This Unit modifies the Rating of its DEF Characteristic by {X} when targeted by an Enemy Unit’s Strike Action." };
            UnitSpecialAbility dutiful = new() { Name = "Dutiful", ValueX = "", Description = "While within 2” of a Scenario Objective counter, this Unit receive a +1 bonus to its CC, MW, and LD Ratings." };
            UnitSpecialAbility entrenched = new() { Name = "Entrenched", Description = "If this Unit is not placed on a Terrain Feature during Game Setup, place a counter next to it to denote it is entrenched. While entrenched, this Unit receives a -1 bonus to its DEF Characteristic and a +2 AR bonus for Armor Tests in response to Shoot Actions. Remove the entrenched counter if this Unit moves for any reason." };
            UnitSpecialAbility evasive2 = new() { Name = "Evasive", ValueX = "2", Description = "This Unit modifies the Rating of its DEF Characteristic by {X} when targeted by an Enemy Unit’s Shoot Action." };
            UnitSpecialAbility executioner1 = new() { Name = "Executioner", ValueX = "1", Description = "This Unit’s Strike Actions score Critical Successes on a natural roll of {X} or lower, instead of only on a natural roll of “1.”" };
            UnitSpecialAbility executioner2 = new() { Name = "Executioner", ValueX = "2", Description = "This Unit’s Strike Actions score Critical Successes on a natural roll of {X} or lower, instead of only on a natural roll of “1.”" };
            UnitSpecialAbility executioner4 = new() { Name = "Executioner", ValueX = "4", Description = "This Unit’s Strike Actions score Critical Successes on a natural roll of {X} or lower, instead of only on a natural roll of “1.”" };
            UnitSpecialAbility executioner5 = new() { Name = "Executioner", ValueX = "5", Description = "This Unit’s Strike Actions score Critical Successes on a natural roll of {X} or lower, instead of only on a natural roll of “1.”" };
            UnitSpecialAbility faith1 = new() { Name = "Faith", ValueX = "8", Description = "During Game Setup, this Unit adds {X} tokens to their Force’s central Faith Pool. At any time, a Friendly Unit with this Unit Special Ability can spend a token from the pool to re-roll any of their Tests." };
            UnitSpecialAbility faith2 = new() { Name = "Faith", ValueX = "10", Description = "During Game Setup, this Unit adds {X} tokens to their Force’s central Faith Pool. At any time, a Friendly Unit with this Unit Special Ability can spend a token from the pool to re-roll any of their Tests." };
            UnitSpecialAbility fearless = new() { Name = "Fearless", Description = "This Unit may be assigned a Reaction Counter even when it currently has a Pinned Counter." };
            UnitSpecialAbility fierceCharge = new() { Name = "Fierce Charge", Description = "This Unit gains an additional +2 DAM on Strike Actions performed as part of a Charge Action." };
            UnitSpecialAbility firingStance = new() { Name = "Firing Stance", Description = "This Unit ignores the Recoil Weapon Special Ability when making a Shoot Action." };
            UnitSpecialAbility firstAid8 = new() { Name = "First Aid", ValueX = "8", Description = "Once during this Unit’s Activation, immediately before or after declaring or performing an Action, this Unit, if in base-to-base contact with a Friendly Unit with one or more  Copyright Res Nova LLC, 2024 Wound Counters, may attempt a TN(X) Test. Success removes a Wound Counter and a natural roll of a “1” removes all Wound Counters. Other Friendly Units within 6\" of this Unit are never assigned more than one Wound Counter as a result of a failed Armor Test." };
            UnitSpecialAbility firstAid10 = new() { Name = "First Aid", ValueX = "10", Description = "Once during this Unit’s Activation, immediately before or after declaring or performing an Action, this Unit, if in base-to-base contact with a Friendly Unit with one or more  Copyright Res Nova LLC, 2024 Wound Counters, may attempt a TN(X) Test. Success removes a Wound Counter and a natural roll of a “1” removes all Wound Counters. Other Friendly Units within 6\" of this Unit are never assigned more than one Wound Counter as a result of a failed Armor Test." };
            UnitSpecialAbility firstAid11 = new() { Name = "First Aid", ValueX = "11", Description = "Once during this Unit’s Activation, immediately before or after declaring or performing an Action, this Unit, if in base-to-base contact with a Friendly Unit with one or more  Copyright Res Nova LLC, 2024 Wound Counters, may attempt a TN(X) Test. Success removes a Wound Counter and a natural roll of a “1” removes all Wound Counters. Other Friendly Units within 6\" of this Unit are never assigned more than one Wound Counter as a result of a failed Armor Test." };
            UnitSpecialAbility firstAid12 = new() { Name = "First Aid", ValueX = "12", Description = "Once during this Unit’s Activation, immediately before or after declaring or performing an Action, this Unit, if in base-to-base contact with a Friendly Unit with one or more  Copyright Res Nova LLC, 2024 Wound Counters, may attempt a TN(X) Test. Success removes a Wound Counter and a natural roll of a “1” removes all Wound Counters. Other Friendly Units within 6\" of this Unit are never assigned more than one Wound Counter as a result of a failed Armor Test." };
            UnitSpecialAbility firstAid13 = new() { Name = "First Aid", ValueX = "13", Description = "Once during this Unit’s Activation, immediately before or after declaring or performing an Action, this Unit, if in base-to-base contact with a Friendly Unit with one or more  Copyright Res Nova LLC, 2024 Wound Counters, may attempt a TN(X) Test. Success removes a Wound Counter and a natural roll of a “1” removes all Wound Counters. Other Friendly Units within 6\" of this Unit are never assigned more than one Wound Counter as a result of a failed Armor Test." };
            UnitSpecialAbility firstStrike = new() { Name = "First Strike", Description = "This Unit’s Weapons receive a x1 Damage Multiplier for Strike Actions targeting Enemy Units that have not been assigned an Activation Counter this Turn." };
            UnitSpecialAbility flight = new() { Name = "Flight", Description = "This Unit may perform the Fly Action." };
            UnitSpecialAbility focusedFire = new() { Name = "Focused Fire", Description = "This Unit’s Shoot Actions with weapons that do not have the Cloud, Explosive, or Projected Weapon Special Abilities gain a cumulative +1 DAM for each prior Shoot Action Test Success against the same Target this Activation." };
            UnitSpecialAbility group2 = new() { Name = "Group", ValueX = "2", Description = "During Game Setup, {X} number of Units of this Unit Type must be deployed at the same time all within 6” of each other. When any Unit of this Unit Type is Activated, {X} Units of this Unit Type without an Activation Counter must be Activated as a single Activation, the player choosing the order in which each of these Units is Activated, performing the Actions of each Unit independent of the other Units in the Group. If a player cannot deploy or activate X number of Units of this Unit Type, the player must deploy or activate as many as possible." };
            UnitSpecialAbility group3 = new() { Name = "Group", ValueX = "3", Description = "During Game Setup, {X} number of Units of this Unit Type must be deployed at the same time all within 6” of each other. When any Unit of this Unit Type is Activated, {X} Units of this Unit Type without an Activation Counter must be Activated as a single Activation, the player choosing the order in which each of these Units is Activated, performing the Actions of each Unit independent of the other Units in the Group. If a player cannot deploy or activate X number of Units of this Unit Type, the player must deploy or activate as many as possible." };
            UnitSpecialAbility guerilla5 = new() { Name = "Guerilla", ValueX = "5", Description = "When this Unit performs an Ambush Reaction, perform a Test with a TN equal to X. On a Success, this Unit does not receive an Activation Counter." };
            UnitSpecialAbility gymnastic = new() { Name = "Gymnastic", Description = "This Unit halves the distance of any fall and treats all Terrain Features as having the Climbable Terrain Trait." };
            UnitSpecialAbility honorbound = new() { Name = "Honorbound", Description = "This Unit never gains the bonus for Overwhelming Numbers." };
            UnitSpecialAbility impact2 = new() { Name = "Impact", ValueX = "2", Description = "After this Unit has completed a Fly Action, all other Units within {X} inches of this Unit must Succeed at a TN(LD) Test or receive a Reaction Counter." };
            UnitSpecialAbility inspire = new() { Name = "Inspire", Description = "If this Unit is neither Engaged nor has a Pinned Counter, it may spend an Action to remove a Reaction or Pinned Counter from a Friendly Unit within 12” and non-Blocked LOS." };
            UnitSpecialAbility nervesOfSteel = new() { Name = "Nerves of Steel", Description = "This Unit may declare an Action that moves them closer to enemies in LOS even if they have a Pinned Counter." };
            UnitSpecialAbility networked = new() { Name = "Networked", Description = "Before declaring a Reaction, this Unit may relocate one of its Reaction Counters to a Friendly Unit with the Networked Unit Special Ability within 8” that doesn’t currently have a Reaction Counter. If this Unit also has the Command Unit Special Ability, it may ignore the LOS requirements when using Command to target a Friendly Unit that also has the Networked Unit Special Ability." };
            UnitSpecialAbility packHunterPS = new() { Name = "Pack Hunter", ValueX = "Praetorian Stalker", Description = "While within 8” of another Unit of Unit Type {X} and possessing no more than one Reaction Counter, this Unit may be assigned a Reaction Counter." };
            UnitSpecialAbility preciseSenses = new() { Name = "Precise Senses", Description = "This Unit ignores the Camouflage Unit Special Ability when performing Shoot Action Tests." };
            UnitSpecialAbility rebreather0 = new() { Name = "Rebreather", ValueX = "0", Description = "When performing an Armor Test against a weapon with the Cloud (X) Weapon Special Ability, this Unit reduces to {X} the DAM Rating of that weapon." };
            UnitSpecialAbility rebreather2 = new() { Name = "Rebreather", ValueX = "2", Description = "When performing an Armor Test against a weapon with the Cloud (X) Weapon Special Ability, this Unit reduces to {X} the DAM Rating of that weapon." };
            UnitSpecialAbility rebreather3 = new() { Name = "Rebreather", ValueX = "3", Description = "When performing an Armor Test against a weapon with the Cloud (X) Weapon Special Ability, this Unit reduces to {X} the DAM Rating of that weapon." };
            UnitSpecialAbility rebreather5 = new() { Name = "Rebreather", ValueX = "5", Description = "When performing an Armor Test against a weapon with the Cloud (X) Weapon Special Ability, this Unit reduces to {X} the DAM Rating of that weapon." };
            UnitSpecialAbility recruit = new() { Name = "Recruit", Description = "This Unit must succeed at a TN(LD) Test to remove a Pinned Counter as part of a Rally Action." };
            UnitSpecialAbility relentless = new() { Name = "Relentless", Description = "This Unit cannot receive Pinned Counters." };
            UnitSpecialAbility scoutAhead = new() { Name = "Scout Ahead", Description = "This Unit is not deployed during Game Setup. Instead, when Activated in the first Turn, it spends its first Action to be deployed anywhere on the Battlefield other than the Enemy Force’s Deployment Area and not within 6” of an Enemy Unit or a Scenario Objective counter." };
            UnitSpecialAbility shadowed = new() { Name = "Shadowed", Description = "Enemy Units may not declare an Ambush Reaction in response to this Unit’s Move or Charge Actions or Reposition." };
            UnitSpecialAbility shakeItOff = new() { Name = "Shake It Off", Description = "This Unit automatically removes any Pinned Counters it possesses during the End Turn Phase." };
            UnitSpecialAbility shift = new() { Name = "Shift", Description = "This Unit may, as an Action once per its Activation, be removed from the Battlefield and then placed onto the Battlefield at a location up to its MV Rating in inches away in any direction, as long as the location satisfies the Model Placement rules. Removal from the Battlefield does not constitute Movement, but placement does." };
            UnitSpecialAbility tactical = new() { Name = "Tactical", Description = "Once per Turn, this Unit may be assigned a Reaction Counter even if it already possesses one or more Reaction Counters while within 2” of a Scenario Objective. This model gains +4 LD whenever using the Interact Action." };
            UnitSpecialAbility trailblaze = new() { Name = "Trailblaze", Description = "This Unit ignores the Rough Terrain Trait." };
            UnitSpecialAbility trainingC1VR = new() { Name = "Training", ValueX = "Camouflage(1)", ValueY = "Venusian Ranger", Description = "This Unit adds the {X} Unit Special Ability to all other Friendly Units of Y Unit Type(s)." };
            UnitSpecialAbility volatileEnd18 = new() { Name = "Volatile End (X, Y)", ValueX = "1", ValueY = "8", Description = "When this Unit is removed from the Battlefield as a Casualty, all Units within X” suffer a DAM Y hit." };
            UnitSpecialAbility zombie = new() { Name = "Zombie", Description = "This Unit may never receive a Reaction Counter. Additionally, unless this Unit is within range of a Friendly Unit with the Controller Unit Special Ability when Activated, this Unit may perform only the Charge, Move, Shoot, and Strike Actions and may not Reposition." };
            UnitSpecialAbility intimidate6 = new() { Name = "Intimidate", ValueX = "6", Description = "All Enemy Units within {X} inches must roll a TN(LD) Test to remove a Pinned Counter as part of a Rally Action." };
            UnitSpecialAbility advancedDeploy = new() { Name = "Advanced Deploy", Description = "In the Start Turn phase of the 1st Turn, this Unit may be relocated to anywhere within 6\", includng outside this Force's Deployment Area" };

            List<UnitSpecialAbility> unitSpecialAbilities = [advancedDeploy, rebreather3, intimidate6, aggressive, art2, art4, awareness, blitz, blur2, blur3, camaraderie, camouflage1, camouflage2, commandVR, commandBL, commandEM, commandHS, commandFM, commandSS, commandMB, commandFB, commandCS, commandMM, commandBB, commandGL, commandTR, commandWC, commandHM, commandSB, commandSM, commandMT, commandAB, commandUL, commandNM, commandULNM1,  commandAA,
                controller3, controller6, controller9, controller12, determination, dodge, driven, duelist1, duelist2, duelist3, dutiful, entrenched, evasive2, executioner1, executioner2, executioner4, executioner5, faith1, faith2, fearless, fierceCharge, firingStance, firstAid8, firstAid10, firstAid11, firstAid12, firstAid13, firstStrike, flight, focusedFire, group2, group3, guerilla5, gymnastic, honorbound, impact2, inspire, nervesOfSteel, networked, packHunterPS, preciseSenses, rebreather0, rebreather2, rebreather5, recruit, relentless, scoutAhead, shadowed, shakeItOff, shift, tactical, trailblaze, trainingC1VR, volatileEnd18, zombie];
            context.UnitSpecialAbilities.AddRange(unitSpecialAbilities);

            Weapon punisherShortSword = new() { Name = "Punisher Shortsword", CCMod = 1, CCDam = 7, DynamicDAM = true, CritFail = 20, WeaponSpecialAbilities = [reach1, vicious] };
            Weapon mp105HG = new() { Name = "MP-105 Handgun", CCMod = 1, CCDam = 12, ShortRange = 6, ShortRangeDam = 12, ShortRangeMod = 1, LongRange = 18, LongRangeDam = 12, LongRangeMod = -12, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon punisherHandgun = new() { Name = "Punisher Handgun", CCMod = -2, CCDam = 12, ShortRange = 6, ShortRangeDam = 13, ShortRangeMod = 0, LongRange = 18, LongRangeDam = 12, LongRangeMod = -2, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon mp103SMG = new() { Name = "MP-103 SMG", ShortRange = 6, ShortRangeDam = 12, ShortRangeMod = 0, LongRange = 18, LongRangeDam = 11, LongRangeMod = -1, CritFail = 19, WeaponSpecialAbilities = [burst2] };
            Weapon hg14SG = new() { Name = "HG-13 Shotgun", ShortRange = 6, ShortRangeDam = 13, ShortRangeMod = 1, LongRange = 18, LongRangeDam = 11, LongRangeMod = -1, CritFail = 20, SRDamageMultiplier = 2, WeaponSpecialAbilities = [] };
            Weapon ag17AR = new() { Name = "AG-17 Assault Rifle", ShortRange = 12, ShortRangeDam = 12, ShortRangeMod = 0, LongRange = 36, LongRangeDam = 12, LongRangeMod = -1, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon psg99SR = new() { Name = "PSG-99 Sniper Rifle", ShortRange = 24, ShortRangeMod = 1, ShortRangeDam = 14, LongRange = 48, LongRangeDam = 13, LongRangeMod = 1, CritFail = 20, WeaponSpecialAbilities = [accurate3] };
            Weapon mg80HMG = new() { Name = "MG-80 HGM", ShortRange = 24, ShortRangeMod = 0, ShortRangeDam = 14, LongRange = 48, LongRangeDam = 13, LongRangeMod = 1, CritFail = 19, WeaponSpecialAbilities = [burst3, recoil4] };
            Weapon gehennaPuker = new() { Name = "Gehenna Puker", ShortRange = 10, ShortRangeMod = 3, ShortRangeDam = 13, CritFail = 17, WeaponSpecialAbilities = [continuous, projected] };
            Weapon frags = new() { Name = "Frag Grenades", LongRange = 4, LongRangeDam = 11, LongRangeMod = -1, CritFail = 20, WeaponSpecialAbilities = [explosive2, indirect] };
            Weapon smokes = new() { Name = "Smoke Grenades", LongRange = 4, LongRangeMod = 0, CritFail = 20, WeaponSpecialAbilities = [cloud2, indirect, smoke] };
            Weapon mg40LMG = new() { Name = "MG-40 LMG", ShortRange = 12, ShortRangeMod = 0, ShortRangeDam = 13, LongRange = 36, LongRangeDam = -1, LongRangeMod = 13, CritFail = 19, WeaponSpecialAbilities = [burst2, recoil3] };
            Weapon ceremonialBlades = new() { Name = "Ceremonial Blades", CCMod = 2, CCDam = 8, CritFail = 20, DynamicDAM = true, WeaponSpecialAbilities = [reach1] };
            Weapon m13HG = new() { Name = "M13 Handgun", CCMod = 1, CCDam = 12, ShortRange = 6, ShortRangeMod = 0, ShortRangeDam = 12, LongRange = 18, LongRangeDam = 11, LongRangeMod = -2, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon car24SMG = new() { Name = "CAR-24 SMG", ShortRange = 6, ShortRangeMod = 0, ShortRangeDam = 12, LongRange = 18, LongRangeDam = 11, LongRangeMod = -1, CritFail = 19, WeaponSpecialAbilities = [burst2] };
            Weapon m516SSG = new() { Name = "M516 Shotgun", ShortRange = 6, ShortRangeMod = 1, ShortRangeDam = 13, LongRange = 18, LongRangeMod = -3, LongRangeDam = 11, CritFail = 20, SRDamageMultiplier = 2, WeaponSpecialAbilities = [] };
            Weapon m50AR = new() { Name = "M-50 Assault Rifle", ShortRange = 12, ShortRangeMod = 0, ShortRangeDam = 13, LongRange = 36, LongRangeMod = -2, LongRangeDam = 13, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon dpat9RL = new() { Name = "DPAT-9 Rocket Launcher", ShortRange = 24, ShortRangeMod = -1, ShortRangeDam = 12, LongRange = 48, LongRangeMod = -3, LongRangeDam = 12, CritFail = 18, WeaponSpecialAbilities = [explosive2, suppressive] };
            Weapon underslungGL = new() { Name = "Underslung Grenade Launcher", ShortRange = 12, ShortRangeMod = 0, ShortRangeDam = 11, LongRange = 36, LongRangeMod = -1, LongRangeDam = 11, CritFail = 18, WeaponSpecialAbilities = [explosive2, indirect] };
            Weapon chainripper = new() { Name = "Chainripper", CCMod = -1, CCDam = 13, CCDamageMultiplier = 2, CritFail = 19, WeaponSpecialAbilities = [continuous, reach1] };
            Weapon bootknife = new() { Name = "Boot Knife", CCMod = 0, CCDam = 4, DynamicDAM = true, WeaponSpecialAbilities = [] };
            Weapon M606LMG = new() { Name = "M606 LMG", ShortRange = 12, ShortRangeMod = 0, ShortRangeDam = 13, LongRange = 24, LongRangeMod = -1, LongRangeDam = 13, CritFail = 19, WeaponSpecialAbilities = [burst2, recoil3] };
            Weapon csa404Sword = new() { Name = "CSA-404 Sword", CCMod = 1, CCDam = 6, DynamicDAM = true, CritFail = 20, WeaponSpecialAbilities = [reach1, vicious] };
            Weapon p1000HG = new() { Name = "P1000 Handgun", CCMod = 0, CCDam = 12, ShortRange = 6, ShortRangeMod = 1, ShortRangeDam = 12, LongRange = 18, LongRangeMod = -1, LongRangeDam = 12, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon sasG72001Shotgun = new() { Name = "SA-SG72001 Shotgun", ShortRange = 6, ShortRangeMod = 1, ShortRangeDam = 13, LongRange = 18, LongRangeMod = -2, LongRangeDam = 12, CritFail = 20, SRDamageMultiplier = 2, WeaponSpecialAbilities = [] };
            Weapon ar3000 = new() { Name = "AR-3000 Assault Rifle", ShortRange = 12, ShortRangeMod = 0, ShortRangeDam = 13, LongRange = 36, LongRangeMod = -1, LongRangeDam = 13, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon sSW4200PHMG = new() { Name = "SSW4200P HMG", ShortRange = 24, ShortRangeMod = 1, ShortRangeDam = 13, LongRange = 48, LongRangeMod = -2, LongRangeDam = 13, CritFail = 20, WeaponSpecialAbilities = [burst3, recoil4] };
            Weapon flashGrenades = new() { Name = "Flash Grenades", LongRange = 4, LongRangeMod = 0, CritFail = 20, WeaponSpecialAbilities = [concussive, explosive3, indirect] };
            Weapon bladeBayonet = new() { Name = "Blade / Bayonet", CCMod = 0, CCDam = 5, DynamicDAM = true, CritFail = 20, WeaponSpecialAbilities = [thrust3] };
            Weapon aggressorHG = new() { Name = "Aggressor Handgun", CCMod = -1, CCDam = 12, ShortRange = 6, ShortRangeMod = 0, ShortRangeDam = 12, LongRange = 18, LongRangeMod = -3, LongRangeDam = 11, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon interceptorSMG = new() { Name = "Interceptor SMG", ShortRange = 6, ShortRangeMod = 0, ShortRangeDam = 12, LongRange = 18, LongRangeMod = -1, LongRangeDam = 12, CritFail = 19, WeaponSpecialAbilities = [burst2] };
            Weapon mandibleSG = new() { Name = "Mandible Shotgun", ShortRange = 6, ShortRangeMod = 0, ShortRangeDam = 14, LongRange = 18, LongRangeMod = -3, LongRangeDam = 12, CritFail = 20, SRDamageMultiplier = 2, WeaponSpecialAbilities = [] };
            Weapon invaderAR = new() { Name = "Invader Assault Rifle", ShortRange = 12, ShortRangeMod = 0, ShortRangeDam = 14, LongRange = 36, LongRangeMod = -2, LongRangeDam = 13, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon plasmaCB = new() { Name = "Plasma Carbine", ShortRange = 8, ShortRangeMod = 0, ShortRangeDam = 12, LongRange = 24, LongRangeMod = -1, LongRangeDam = 12, CritFail = 18, WeaponSpecialAbilities = [continuous, explosive1] };
            Weapon destroyerLMG = new() { Name = "Destroyer LMG", ShortRange = 12, ShortRangeMod = 0, ShortRangeDam = 14, LongRange = 36, LongRangeMod = -2, LongRangeDam = 13, CritFail = 19, WeaponSpecialAbilities = [burst2, recoil4] };
            Weapon chargerHMG = new() { Name = "Charger HMG", ShortRange = 24, ShortRangeMod = 1, ShortRangeDam = 14, LongRange = 48, LongRangeMod = -3, LongRangeDam = 14, CritFail = 18, WeaponSpecialAbilities = [burst3, recoil4] };
            Weapon southpawRPG = new() { Name = "Southpaw RPG", ShortRange = 24, ShortRangeMod = -2, ShortRangeDam = 13, LongRange = 48, LongRangeMod = -4, LongRangeDam = 13, CritFail = 18, WeaponSpecialAbilities = [explosive2, suppressive] };
            Weapon plasmaGrenade = new() { Name = "Plasma Grenade", LongRange = 4, LongRangeMod = -1, LongRangeDam = 12, CritFail = 19, WeaponSpecialAbilities = [explosive1, continuous, indirect] };
            Weapon battleAx = new() { Name = "Battle Axe", CCMod = -1, CCDam = 9, DynamicDAM = true, CritFail = 20, WeaponSpecialAbilities = [multiStrike2, reach2] };
            Weapon claymore = new() { Name = "Claymore", CCMod = -1, CCDam = 9, DynamicDAM = true, CritFail = 20, WeaponSpecialAbilities = [thrust2, reach2] };
            Weapon poisonedPunisherShortSword = new() { Name = "Poisoned Punisher Shortsword", CCMod = 1, CCDam = 7, DynamicDAM = true, CritFail = 20, CCDamageMultiplier = 2, WeaponSpecialAbilities = [reach1, vicious] };
            Weapon silencedPunisherHandgun = new() { Name = "Silenced Punisher Handgun", CCMod = -2, CCDam = 12, ShortRange = 6, ShortRangeDam = 13, ShortRangeMod = 0, LongRange = 18, LongRangeDam = 12, LongRangeMod = -2, CritFail = 20, WeaponSpecialAbilities = [silenced] };
            Weapon windriderSMG = new() { Name = "Windrider SMG", ShortRange = 6, ShortRangeMod = 1, ShortRangeDam = 11, LongRange = 0, LongRangeMod = 0, LongRangeDam = 0, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon airbrushSG = new() { Name = "Airbrush Shotgun", ShortRange = 6, ShortRangeMod = 0, ShortRangeDam = 12, SRDamageMultiplier = 2, LongRange = -3, LongRangeMod = 11, LongRangeDam = 0, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon shogunAR = new() { Name = "Shogun Assault Rifle", ShortRange = 12, ShortRangeMod = 1, ShortRangeDam = 12, LongRange = 36, LongRangeMod = 0, LongRangeDam = 12, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon dragonfireHMG = new() { Name = "Dragonfire HMG", ShortRange = 24, ShortRangeMod = 0, ShortRangeDam = 14, LongRange = 48, LongRangeMod = 0, LongRangeDam = -1, CritFail = 18, WeaponSpecialAbilities = [burst3, recoil4] };
            Weapon archerSR = new() { Name = "Archer Sniper Rifle", ShortRange = 24, ShortRangeMod = 2, ShortRangeDam = 13, LongRange = 48, LongRangeMod = 1, LongRangeDam = 13, CritFail = 20, WeaponSpecialAbilities = [accurate3] };
            Weapon mortisBlade = new() { Name = "Mortis Blade", CCMod = 1, CCDam = 7, CritFail = 20, DynamicDAM = true, WeaponSpecialAbilities = [reach1, vicious] };
            Weapon avengerSword = new() { Name = "Avenger Sword", CCMod = 0, CCDam = 8, CritFail = 20, DynamicDAM = true, WeaponSpecialAbilities = [reach1, thrust2] };
            Weapon delivererBattleBlade = new() { Name = "Deliverer Battleblade", CCMod = -1, CCDam = 9, DynamicDAM = true, CritFail = 20, WeaponSpecialAbilities = [reach2, thrust2] };
            Weapon silencedNemesisHG = new() { Name = "Silenced Nemesis Handgun", CCMod = -1, CCDam = 11, ShortRange = 6, ShortRangeMod = 1, ShortRangeDam = 11, LongRange = 18, LongRangeMod = 0, LongRangeDam = 11, CritFail = 20, WeaponSpecialAbilities = [silenced] };
            Weapon avalanceHG = new() { Name = "Avalance Handgun", CCMod = -1, CCDam = 12, ShortRange = 6, ShortRangeMod = 1, ShortRangeDam = 13, LongRange = 18, LongRangeMod = 0, LongRangeDam = 12, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon r75RetributorCB = new() { Name = "R75 Retributor Carbine", ShortRange = 8, ShortRangeMod = 2, ShortRangeDam = 12, LongRange = 24, LongRangeMod = 1, LongRangeDam = 12, CritFail = 20, WeaponSpecialAbilities = [accurate2] };
            Weapon eruptorLMG = new() { Name = "Eruptor LMG", ShortRange = 12, ShortRangeMod = 1, ShortRangeDam = 13, LongRange = 36, LongRangeMod = 0, LongRangeDam = 12, CritFail = 18, WeaponSpecialAbilities = [burst2, recoil3] };
            Weapon eruptorFT = new() { Name = "Eruptor Flamethrower", ShortRange = 10, ShortRangeMod = 3, ShortRangeDam = 11, LongRange = 0, CritFail = 18, WeaponSpecialAbilities = [continuous, projected] };
            Weapon ac40Justifier = new() { Name = "AC-40 Justifier", CCMod = 1, CCDam = 11, CCDamageMultiplier = 2, SRDamageMultiplier = 2, ShortRange = 10, ShortRangeMod = 2, ShortRangeDam = 12, LongRange = 24, LongRangeMod = 1, LongRangeDam = 12, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon sectionerBayonet = new() { Name = "Sectioner Bayonet", CCMod = 1, CCDam = 5, CritFail = 19, WeaponSpecialAbilities = [thrust5] };
            Weapon skalakSword = new() { Name = "Skalak Sword", CCMod = 0, CCDam = 7, DynamicDAM = true, CritFail = 20, WeaponSpecialAbilities = [reach1] };
            Weapon azogar = new() { Name = "Azogar", CCMod = -2, CCDam = 7, DynamicDAM = true, CritFail = 19, WeaponSpecialAbilities = [multiStrike3, reach2, vicious] };
            Weapon voricheHG = new() { Name = "Voriche Handgun", CCMod = -2, CCDam = 14, ShortRange = 6, ShortRangeMod = 1, ShortRangeDam = 14, LongRange = 18, LongRangeMod = -1, LongRangeDam = 14, CritFail = 19, WeaponSpecialAbilities = [] };
            Weapon kratachAR = new() { Name = "Kratach Assault Rifle", ShortRange = 12, ShortRangeMod = 0, ShortRangeDam = 12, LongRange = 36, LongRangeMod = -1, LongRangeDam = 12, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon belzarachAR = new() { Name = "Belzarach Assault Rifle", ShortRange = 12, ShortRangeMod = 0, ShortRangeDam = 14, LongRange = 36, LongRangeMod = -2, LongRangeDam = 14, CritFail = 20, WeaponSpecialAbilities = [] };
            Weapon tormentorFT = new() { Name = "Tormentor Flamethrower", ShortRange = 10, ShortRangeMod = 3, ShortRangeDam = 14, LongRange = 0, CritFail = 17, WeaponSpecialAbilities = [continuous, projected] };
            Weapon incinerator = new() { Name = "Incinerator Flamethrower", ShortRange = 10, ShortRangeMod = 3, ShortRangeDam = 13, LongRange = 0, CritFail = 17, WeaponSpecialAbilities = [continuous, projected] };
            Weapon scytheOfSemai = new() { Name = "Scythe of Semai LMG", ShortRange = 12, ShortRangeMod = 1, ShortRangeDam = 12, LongRange = 36, LongRangeMod = 0, LongRangeDam = 12, CritFail = 18, WeaponSpecialAbilities = [burst3, recoil3] };
            Weapon nazgarothHMG = new() { Name = "Nazgaroth HMG", ShortRange = 24, ShortRangeMod = 1, ShortRangeDam = 15, LongRange = 36, LongRangeMod = -1, LongRangeDam = 15, CritFail = 19, WeaponSpecialAbilities = [burst3, recoil4] };
            Weapon ashnazgarothHMG = new() { Name = "Ashzazgaroth HMG", ShortRange = 24, ShortRangeMod = 1, ShortRangeDam = 13, LongRange = 36, LongRangeMod = -3, LongRangeDam = 13, CritFail = 19, WeaponSpecialAbilities = [burst4, recoil3] };
            Weapon hellblasterLauncher = new() { Name = "Hellblaster Launcher", ShortRange = 12, ShortRangeMod = 0, ShortRangeDam = 13, LongRange = 36, LongRangeMod = -1, LongRangeDam = 13, CritFail = 19, WeaponSpecialAbilities = [explosive2, suppressive, vicious] };
            Weapon carcassGrenadeLauncher = new() { Name = "Carcass Grenade Launcher", ShortRange = 12, ShortRangeMod = -1, ShortRangeDam = 11, LongRange = 36, LongRangeMod = -4, LongRangeDam = 11, CritFail = 18, WeaponSpecialAbilities = [explosive2, continuous, indirect] };
            Weapon gasGrenade = new() { Name = "Gas Grenades", LongRange = 4, LongRangeMod = -1, LongRangeDam = 9, CritFail = 19, WeaponSpecialAbilities = [cloud2, continuous, indirect, smoke] };
            Weapon devouringDarkness = new() { Name = "The Devouring Darkness", ShortRange = 18, ShortRangeMod = -4, ShortRangeDam = 4, CritFail = 20, WeaponSpecialAbilities = [symmetry, terrifying] };
            Weapon necrotalons = new() { Name = "Necrotalons", CCMod = 1, CCDam = 4, DynamicDAM = true, CritFail = 20, WeaponSpecialAbilities = [flurry2, reach1] };
            Weapon BlutarchHC = new() { Name = "Blutarch Hand Cannon", ShortRange = 6, ShortRangeMod = 0, ShortRangeDam = 15, LongRange = 18, LongRangeMod = -2, LongRangeDam = 14, CritFail = 19, WeaponSpecialAbilities = [burst2, recoil1] };
            Weapon soulshearer = new() { Name = "Soulshearer", ShortRange = 12, ShortRangeMod = 0, ShortRangeDam = 5, LongRange = 36, LongRangeMod = -2, LongRangeDam = 5, CritFail = 19, WeaponSpecialAbilities = [gruesome3, terrifying, vicious] };
            Weapon soullessShriek = new() { Name = "Soulless Shriek", ShortRange = 8, ShortRangeMod = 2, ShortRangeDam = 10, WeaponSpecialAbilities = [concussive, projected, symmetry] };
            Weapon handOfDeath = new() { Name = "Hand of Death", ShortRange = 0, ShortRangeMod = 2, ShortRangeDam = 15, CritFail = 20, WeaponSpecialAbilities = [accurate3, symmetry] };
            Weapon heavyTemplarBlade = new() { Name = "Heavy Templar Blade", CCMod = -1, DynamicDAM = true, CCDam = 8, CritFail = 20, WeaponSpecialAbilities = [multiStrike3, reach2] };
            Weapon templarMartialis = new() { Name = "Templar Martialis", CCMod = 0, CCDam = 6, DynamicDAM = true, CritFail = 20, WeaponSpecialAbilities = [multiStrike2, reach1] };
            Weapon metaCannon = new() { Name = "Meta Cannon", ShortRange = 18, ShortRangeMod = 0, ShortRangeDam = 12, SRDamageMultiplier = 2, LongRange = 36, LongRangeMod = -2, LongRangeDam = 12, LRDamageMultiplier = 2, CritFail = 19, WeaponSpecialAbilities = [concussive, explosive1, vicious] };

            List<Weapon> weapons = [punisherShortSword, mp105HG, punisherHandgun, mp103SMG, hg14SG, ag17AR, psg99SR, mg80HMG, gehennaPuker, frags, smokes, mg40LMG, ceremonialBlades, m13HG, car24SMG, m516SSG, m50AR, dpat9RL, underslungGL, chainripper, bootknife, M606LMG, csa404Sword, p1000HG, sasG72001Shotgun, ar3000, sSW4200PHMG, flashGrenades, bladeBayonet, aggressorHG, interceptorSMG, mandibleSG, invaderAR, plasmaCB, destroyerLMG, chargerHMG, southpawRPG, plasmaGrenade, battleAx, claymore, poisonedPunisherShortSword, silencedPunisherHandgun, windriderSMG, airbrushSG, shogunAR, dragonfireHMG, archerSR, mortisBlade, avengerSword, delivererBattleBlade, silencedNemesisHG, avalanceHG, r75RetributorCB, eruptorLMG, eruptorFT, ac40Justifier, sectionerBayonet, skalakSword, azogar, voricheHG, kratachAR, belzarachAR, tormentorFT, incinerator, scytheOfSemai, nazgarothHMG, ashnazgarothHMG, hellblasterLauncher, carcassGrenadeLauncher, gasGrenade, devouringDarkness, necrotalons, BlutarchHC, soulshearer, soullessShriek, handOfDeath, heavyTemplarBlade, templarMartialis, metaCannon];

            context.Weapons.AddRange(weapons);

            context.SaveChanges();
            List<Unit> units = new() { };
            // Seed data for BH Units
            Unit venusianRangerTrooper = new("Bauhaus", "Venusian Ranger", ["Trooper"], 4, 0, 4, 14, 12, 6, 0, 22, 2, 12, 12, 30, null, 0, new List<UnitSpecialAbility> { camouflage1, determination }, ["Bauhaus"]);
            venusianRangerTrooper.AddWeapon([ag17AR, mp105HG]);
            units.Add(venusianRangerTrooper);
            Unit venusianRangerMedic = new("Bauhaus", "Venusian Ranger", ["Medic","Specialist"], 4, 0, 4, 14, 12, 6, 0, 22, 2, 12, 12, 30, "Venusian Ranger", 1, new List<UnitSpecialAbility> { camouflage1, determination, firstAid12 }, ["Bauhaus"]);
            venusianRangerMedic.AddWeapon([ag17AR, mp105HG]);
            units.Add(venusianRangerMedic);
            Unit venusianRangerSupport = new("Bauhaus", "Venusian Ranger", ["Support"], 4, -3, 4, 14, 12, 6, 0, 22, 2, 12, 12, 30, null, 0, new List<UnitSpecialAbility> { camouflage1, determination }, ["Bauhaus"]);
            venusianRangerSupport.AddWeapon([mg80HMG, mp105HG]);
            units.Add(venusianRangerSupport);
            Unit venusianRangerLeader = new("Bauhaus", "Venusian Ranger", ["Leader"], 5, 3, 4, 14, 12, 6, 0, 22, 2, 12, 12, 30, "Venusian Ranger", 1, new List<UnitSpecialAbility> { camouflage1, determination, commandVR, inspire }, ["Bauhaus"]);
            venusianRangerLeader.AddWeapon([hg14SG, mp105HG]);
            units.Add(venusianRangerLeader);

            Unit blitzerTrooper = new("Bauhaus", "Blitzer", ["Trooper"], 4, 0, 4, 13, 13, 5, -1, 21, 2, 11, 12, 30, null, 0, new List<UnitSpecialAbility> { blitz, trailblaze }, ["Bauhaus"]);
            blitzerTrooper.AddWeapon([mp103SMG, mp105HG]);
            Unit blitzerSupport = new("Bauhaus", "Blitzer", ["Support"], 4, -1, 4, 13, 13, 5, -1, 21, 2, 11, 12, 30, null, 0, new List<UnitSpecialAbility> { blitz, trailblaze }, ["Bauhaus"]);
            blitzerSupport.AddWeapon([gehennaPuker, mp105HG]);
            Unit blitzerSpecialist = new("Bauhaus", "Blitzer", ["Operator","Specialist"], 4, 0, 4, 13, 13, 5, -1, 21, 2, 11, 12, 30, "Blitzer", 1, new List<UnitSpecialAbility> { blitz, trailblaze, tactical }, ["Bauhaus"]);
            blitzerSpecialist.AddWeapon([mp103SMG,mp105HG,frags,smokes]);
            Unit blitzerLeader = new("Bauhaus", "Blitzer", ["Operator", "Specialist"], 5, 2, 4, 13, 13, 5, -1, 21, 2, 11, 12, 30, "Blitzer", 1, new List<UnitSpecialAbility> { blitz, trailblaze, inspire,commandBL }, ["Bauhaus"]);
            blitzerLeader.AddWeapon([mp103SMG, hg14SG]);
            units.AddRange([blitzerTrooper, blitzerSupport, blitzerSpecialist, blitzerLeader]);


            Unit etoilesMortantTrooper = new("Bauhaus", "Etoiles Mortant", ["Trooper"], 4, 0, 5, 12, 15, 6, -2, 20, 2, 13, 13, 30, null, 0, new List<UnitSpecialAbility> { awareness, dodge, evasive2}, ["Bauhaus"]);
            etoilesMortantTrooper.AddWeapon([punisherHandgun, punisherShortSword]);
            Unit etoilesMortantSupport = new("Bauhaus", "Etoiles Mortant", ["Support"], 4, -2, 5, 12, 15, 6, -2, 20, 2, 13, 13, 30, null, 0, new List<UnitSpecialAbility> { awareness, dodge, evasive2 }, ["Bauhaus"]);
            etoilesMortantSupport.AddWeapon([punisherHandgun, psg99SR]);
            Unit etoilesMortantLeader = new("Bauhaus", "Etoiles Mortant", ["Leader"], 5, 2, 5, 12, 15, 6, -2, 20, 2, 13, 13, 30, null, 0, new List<UnitSpecialAbility> { awareness, dodge, evasive2, commandEM, inspire }, ["Bauhaus"]);
            etoilesMortantLeader.AddWeapon([punisherHandgun, punisherShortSword]);
            units.AddRange([etoilesMortantTrooper, etoilesMortantSupport, etoilesMortantLeader]);

            // Seed data for CL Units
            Unit freeMarineTrooper = new("Capitol", "Free Marine", ["Trooper"], 4, 0, 4, 13, 13, 6, 0, 21, 2, 11, 12, 30, null, 0, new List<UnitSpecialAbility> { camouflage2, guerilla5}, ["Capitol"]);
            freeMarineTrooper.AddWeapon([m50AR, punisherShortSword]);
            Unit freeMarineSpecialist = new("Capitol", "Free Marine", ["Medic", "Specialist"], 4, 0, 4, 13, 13, 6, 0, 21, 2, 11, 12, 30, "Free Marine", 1, new List<UnitSpecialAbility> { camouflage2, guerilla5, firstAid11 }, ["Capitol"]);
            freeMarineSpecialist.AddWeapon([m50AR, punisherShortSword]);
            Unit freeMarineSupport = new("Capitol", "Free Marine", ["Support"], 4, -2, 4, 13, 13, 6, 0, 21, 2, 11, 12, 30, "Free Marine", 1, new List<UnitSpecialAbility> { camouflage2, guerilla5 }, ["Capitol"]);
            freeMarineSupport.AddWeapon([dpat9RL, punisherShortSword]);
            Unit freeMarineLeader = new("Capitol", "Free Marine", ["Leader"], 5, 3, 4, 13, 13, 6, 0, 21, 2, 11, 12, 30, "Free Marine", 1, new List<UnitSpecialAbility> { camouflage2, guerilla5, inspire, commandFM }, ["Capitol"]);
            freeMarineLeader.AddWeapon([m50AR, punisherShortSword]);
            units.AddRange([freeMarineTrooper, freeMarineSpecialist, freeMarineSupport, freeMarineLeader]);

           Unit sunsetStrikerTrooper = new("Capitol", "Sunset Striker", ["Trooper"], 4, 0, 4, 12, 15, 5, -1, 21, 2, 12, 13, 30, null, 0, new List<UnitSpecialAbility> { duelist2, fierceCharge}, ["Capitol"]);
            sunsetStrikerTrooper.AddWeapon([m50AR, ceremonialBlades]);
            Unit sunsetStrikerSupport = new("Capitol", "Sunset Striker", ["Support"], 4, -1, 4, 12, 15, 5, -1, 21, 2, 12, 13, 30, null, 0, new List<UnitSpecialAbility> { duelist2, fierceCharge }, ["Capitol"]);
            sunsetStrikerSupport.AddWeapon([gehennaPuker, ceremonialBlades]);
            Unit sunsetStrikerSpecialist = new("Capitol", "Sunset Striker", ["Operator","Specialist"], 4, 0, 4, 12, 15, 5, -1, 21, 2, 12, 13, 30, "Sunset Striker", 1, new List<UnitSpecialAbility> { duelist2, fierceCharge, tactical }, ["Capitol"]);
            sunsetStrikerSpecialist.AddWeapon([m50AR, ceremonialBlades, frags, smokes]);
            Unit sunsetStrikerLeader = new("Capitol", "Sunset Striker", ["Leader"], 5, 3, 4, 12, 15, 5, -1, 21, 2, 12, 13, 30, "Sunset Striker", 1, new List<UnitSpecialAbility> { duelist2, fierceCharge, inspire, commandSS }, ["Capitol"]);
            sunsetStrikerLeader.AddWeapon([m516SSG, ceremonialBlades]);
            units.AddRange([sunsetStrikerTrooper, sunsetStrikerSupport, sunsetStrikerSpecialist, sunsetStrikerLeader]);

            Unit martianBansheeTrooper = new("Capitol", "Martian Banshee", ["Trooper"], 5, 0, 4, 12, 12, 6, 0, 23, 2, 12, 14, 30, null, 0, new List<UnitSpecialAbility> { flight, impact2, nervesOfSteel}, ["Capitol"]);
            martianBansheeTrooper.AddWeapon([car24SMG, m13HG, frags]);
            Unit martianBansheeSupport = new("Capitol", "Martian Banshee", ["Support"], 5, -1, 4, 12, 12, 6, 0, 23, 2, 12, 14, 30, null, 0, new List<UnitSpecialAbility> { flight, impact2, nervesOfSteel }, ["Capitol"]);
            martianBansheeSupport.AddWeapon([gehennaPuker, m13HG, frags]);
            Unit martianBansheeLeader = new("Capitol", "Martian Banshee", ["Leader"], 6, 2, 4, 12, 12, 6, 0, 23, 2, 12, 14, 30, "Martian Banshee", 1, new List<UnitSpecialAbility> { flight, impact2, nervesOfSteel }, ["Capitol"]);
            martianBansheeLeader.AddWeapon([car24SMG, m13HG, frags]);
            units.AddRange([martianBansheeTrooper, martianBansheeSupport, martianBansheeLeader]);

            // Seed data for CT Units
            Unit chasseurTrooper = new("Cybertronic", "Chasseur", ["Trooper"], 4, 0, 4, 13, 13, 9, 1, 24, 2, 10, 11, 30, null, 0, new List<UnitSpecialAbility> { networked}, ["Cybertronic"]);
            chasseurTrooper.AddWeapon([ar3000]);
            Unit chasseurSupport = new("Cybertronic", "Chasseur", ["Support"], 4, -3, 4, 13, 13, 9, 1, 24, 2, 10, 11, 30, null, 0, new List<UnitSpecialAbility> { networked }, ["Cybertronic"]);
            chasseurSupport.AddWeapon([sSW4200PHMG]);
            Unit chasseurSpecialist = new("Cybertronic", "Chasseur", ["Operator","Specialist"], 4, 0, 4, 13, 13, 9, 1, 24, 2, 10, 11, 30, "Chasseur", 1, new List<UnitSpecialAbility> { networked, tactical }, ["Cybertronic"]);
            chasseurSpecialist.AddWeapon([ar3000]);
            Unit chasseurLeader = new("Cybertronic", "Chasseur", ["Leader"], 5, 4, 4, 13, 13, 9, 1, 24, 2, 10, 11, 30, "Chasseur", 1, new List<UnitSpecialAbility> { networked, inspire, commandCS }, ["Cybertronic"]);
            chasseurLeader.AddWeapon([ar3000]);
            units.AddRange([chasseurTrooper, chasseurSupport, chasseurSpecialist, chasseurLeader]);

            Unit mirrormanTrooper = new("Cybertronic", "Mirrorman", ["Trooper"], 4, 0, 5, 12, 15, 8, -1, 21, 2, 10, 11, 30, null, 0, new List<UnitSpecialAbility> { blur2, camouflage2,networked }, ["Cybertronic"]);
            mirrormanTrooper.AddWeapon([csa404Sword, p1000HG]);
            Unit mirrormanLeader = new("Cybertronic", "Mirrorman", ["Leader"], 5, 3, 5, 12, 15, 8, -1, 21, 2, 10, 11, 30, "Mirrorman", 1, new List<UnitSpecialAbility> { blur2, camouflage2, networked, commandMM, inspire }, ["Cybertronic"]);
            mirrormanLeader.AddWeapon([csa404Sword, sasG72001Shotgun, flashGrenades]);
            units.AddRange([mirrormanTrooper, mirrormanLeader]);

            Unit attillaTrooper = new("Cybertronic", "Attilla Mk III Cuirassier", ["Trooper"], 8, 0, 3, 13, 12, 12, 1, 25, 3, 10, 10, 40, null, 0, new List<UnitSpecialAbility> {aggressive, firingStance, nervesOfSteel ,networked }, ["Cybertronic"]);
            attillaTrooper.AddWeapon([ar3000]);
            Unit attillaSupportHMG = new("Cybertronic", "Attilla Mk III Cuirassier", ["Support"], 8, -3, 3, 13, 12, 12, 1, 25, 3, 10, 10, 40, null, 0, new List<UnitSpecialAbility> { aggressive, firingStance, nervesOfSteel, networked }, ["Cybertronic"]);
            attillaSupportHMG.AddWeapon([sSW4200PHMG]);
            Unit attillaSupportFT = new("Cybertronic", "Attilla Mk III Cuirassier", ["Support"], 8, -1, 3, 13, 12, 12, 1, 25, 3, 10, 10, 40, null, 0, new List<UnitSpecialAbility> { aggressive, firingStance, nervesOfSteel, networked }, ["Cybertronic"]);
            attillaSupportHMG.AddWeapon([gehennaPuker]);
            units.AddRange([attillaTrooper, attillaSupportHMG, attillaSupportFT]);

            // Seed data for IM Units
            Unit bloodBerretTrooper = new("Imperial", "Blood Beret", ["Trooper"], 4, 0, 4, 13, 13, 6, -1, 22, 2, 10, 12, 30, null, 0, new List<UnitSpecialAbility> { camouflage1, shakeItOff, rebreather3}, ["Imperial"]);
            bloodBerretTrooper.AddWeapon([aggressorHG, bladeBayonet, invaderAR]);
            Unit bloodBerretSpecialist = new("Imperial", "Blood Beret", ["Medic","Specialist"], 4, 0, 4, 13, 13, 6, -1, 22, 2, 10, 12, 30, "Blood Beret", 1, new List<UnitSpecialAbility> { camouflage1, shakeItOff, rebreather3, firstAid11 }, ["Imperial"]);
            bloodBerretSpecialist.AddWeapon([aggressorHG, bladeBayonet, invaderAR]);
            Unit bloodBerretSupport = new("Imperial", "Blood Beret", ["Support"], 4, -2, 4, 13, 13, 6, -1, 22, 2, 10, 12, 30, null, 0, new List<UnitSpecialAbility> { camouflage1, shakeItOff, rebreather3 }, ["Imperial"]);
            bloodBerretSupport.AddWeapon([aggressorHG, southpawRPG]);
            Unit bloodBerretLeader = new("Imperial", "Blood Beret", ["Leader"], 5, 3, 4, 13, 13, 6, -1, 22, 2, 10, 12, 30, "Blood Beret", 1, new List<UnitSpecialAbility> { camouflage1, shakeItOff, rebreather3, inspire, commandBB }, ["Imperial"]);
            bloodBerretLeader.AddWeapon([aggressorHG, bladeBayonet, invaderAR]);
            units.AddRange([bloodBerretTrooper, bloodBerretSpecialist, bloodBerretSupport, bloodBerretLeader,]);

            Unit goldenLionTrooper = new("Imperial", "Golden Lion", ["Trooper"], 4, 0, 4, 14, 12, 6, 0, 22, 2, 11, 13, 30, null, 0, new List<UnitSpecialAbility> { focusedFire, rebreather3 }, ["Imperial"]);
            goldenLionTrooper.AddWeapon([aggressorHG, interceptorSMG]);
            Unit goldenLionSpecialist = new("Imperial", "Golden Lion", ["Operator", "Specialist"], 4, 0, 4, 14, 12, 6, 0, 22, 2, 11, 13, 30, "Golden Lion", 1, new List<UnitSpecialAbility> { focusedFire, rebreather3, tactical }, ["Imperial"]);
            goldenLionSpecialist.AddWeapon([aggressorHG, plasmaCB, plasmaGrenade, smokes]);
            Unit goldenLionSupport = new("Imperial", "Golden Lion", ["Support"], 4, -3, 4, 14, 12, 6, 0, 22, 2, 11, 13, 30, null, 0, new List<UnitSpecialAbility> { focusedFire, rebreather3 }, ["Imperial"]);
            goldenLionSupport.AddWeapon([aggressorHG, destroyerLMG]);
            Unit goldenLionLeader = new("Imperial", "Golden Lion", ["Leader"], 5, 3, 4, 14, 12, 6, 0, 22, 2, 11, 13, 30, "Golden Lion", 1, new List<UnitSpecialAbility> { focusedFire, rebreather3, inspire, commandGL }, ["Imperial"]);
            goldenLionLeader.AddWeapon([chainripper, plasmaCB]);
            units.AddRange([goldenLionTrooper, goldenLionSpecialist, goldenLionSupport, goldenLionLeader]);

            Unit trencherTrooper = new("Imperial", "Trencher", ["Trooper"], 3, 0, 4, 11, 10, 4, 0, 21, 2, 10, 11, 30, null, 0, new List<UnitSpecialAbility> { rebreather2, entrenched, group2, recruit}, ["Imperial"]);
            trencherTrooper.AddWeapon([aggressorHG, bladeBayonet, invaderAR]);
            Unit trencherSupport = new("Imperial", "Trencher", ["Support"], 3, -3, 4, 11, 10, 4, 0, 21, 2, 10, 11, 30, null, 0, new List<UnitSpecialAbility> { rebreather2, entrenched, group2, recruit }, ["Imperial"]);
            trencherSupport.AddWeapon([aggressorHG, chargerHMG]);
            Unit trencherLeader = new("Imperial", "Trencher", ["Leader"], 4, 3, 4, 11, 10, 4, 0, 21, 2, 10, 11, 30, null, 0, new List<UnitSpecialAbility> { rebreather2, entrenched, group2, recruit, inspire, commandTR }, ["Imperial"]);
            trencherLeader.AddWeapon([aggressorHG, bladeBayonet, invaderAR]);
            units.AddRange([trencherTrooper, trencherSupport, trencherLeader]);

            // Seed data for MI Units
            Unit hatamotoTrooper = new("Mishima", "Hatamoto", ["Trooper"], 4, 0, 4, 13, 16, 6, 0, 23, 2, 11, 13, 30, null, 0, new List<UnitSpecialAbility> { duelist3, executioner2, fearless, honorbound}, ["Mishima"]);
            hatamotoTrooper.AddWeapon([ceremonialBlades, shogunAR]);
            Unit hatamotoSupport = new("Mishima", "Hatamoto", ["Support"], 4, -3, 4, 13, 16, 6, 0, 23, 2, 11, 13, 30, null, 0, new List<UnitSpecialAbility> { duelist3, executioner2, fearless, honorbound }, ["Mishima"]);
            hatamotoSupport.AddWeapon([ceremonialBlades, dragonfireHMG]);
            Unit hatamotoSpecialist = new("Mishima", "Hatamoto", ["Operator","Specialist"],  4, 0, 4, 13, 16, 6, 0, 23, 2, 11, 13, 30, "Hatamoto", 1, new List<UnitSpecialAbility> { duelist3, executioner2, fearless, honorbound, tactical }, ["Mishima"]);
            hatamotoSpecialist.AddWeapon([ceremonialBlades, shogunAR, smokes, frags]);
            Unit hatamotoLeader = new("Mishima", "Hatamoto", ["Leader"], 5, 3, 4, 13, 16, 6, 0, 23, 2, 11, 13, 30, "Hatamoto", 1, new List<UnitSpecialAbility> { duelist3, executioner2, fearless, honorbound, inspire, commandHM }, ["Mishima"]);
            hatamotoLeader.AddWeapon([ceremonialBlades, airbrushSG]);
            units.AddRange([hatamotoTrooper, hatamotoSupport, hatamotoSpecialist, hatamotoLeader]);

            Unit shinobiTrooper = new("Mishima", "Shinobi", ["Trooper"], 4, 0, 5, 13, 14, 5, -1, 21, 2, 12, 11, 30, null, 0, new List<UnitSpecialAbility> { duelist1, shift}, ["Mishima"]);
            shinobiTrooper.AddWeapon([ceremonialBlades, windriderSMG]);
            Unit shinobiSupport = new("Mishima", "Shinobi", ["Support"], 4, 0, 5, 13, 14, 5, -1, 21, 2, 12, 11, 30, null, 0, new List<UnitSpecialAbility> { duelist1, shift, firstAid10}, ["Mishima"]);
            shinobiSupport.AddWeapon([ceremonialBlades, archerSR]);
            Unit shinobiSpecialist = new("Mishima", "Shinobi", ["Medic", "Specialist"], 4, 0, 5, 13, 14, 5, -1, 21, 2, 12, 11, 30, "Shinobi", 1, new List<UnitSpecialAbility> { duelist1, shift }, ["Mishima"]);
            shinobiSpecialist.AddWeapon([ceremonialBlades, windriderSMG, smokes, frags]);
            Unit shinobiLeader = new("Mishima", "Shinobi", ["Leader"], 5, 0, 5, 13, 14, 5, -1, 21, 2, 12, 11, 30, "Shinobi", 1, new List<UnitSpecialAbility> { duelist1, shift, inspire, commandSB }, ["Mishima"]);
            shinobiLeader.AddWeapon([ceremonialBlades, airbrushSG]);
            units.AddRange([shinobiTrooper, shinobiSupport, shinobiSpecialist, shinobiLeader]);

            Unit shadowWalker = new("Mishima", "Shadow Walker", ["Trooper"], 5, 0, 6, 12, 15, 6, -2, 20, 2, 12, 13, 30, null, 0, new List<UnitSpecialAbility> { blur2, camouflage2, fearless, firstStrike, volatileEnd18, tactical}, ["Mishima"]);
            shadowWalker.AddWeapon([poisonedPunisherShortSword, silencedPunisherHandgun, smokes]);
            units.AddRange([shadowWalker]);

            // Seed data for BH Units
            Unit mortificatorTrooper = new("Brotherhood", "Mortificator", ["Trooper", "Seconding"], 6, 0, 6, 14, 16, 6, -2, 20, 2, 13, 13, 30, null, 0, new List<UnitSpecialAbility> { camouflage2, dodge, executioner2, firstStrike, gymnastic, shadowed }, ["Brotherhood", "Seconding"]);
            mortificatorTrooper.AddWeapon([silencedNemesisHG, mortisBlade, smokes]);
            Unit mortificatorTrooperUnique = new("Brotherhood", "Redemtion Assassin", ["Trooper", "Unique", "Seconding"], 7, 0, 6, 15, 17, 6, -2, 20, 2, 13, 15, 30, null, 0, new List<UnitSpecialAbility> {advancedDeploy ,camouflage2, dodge, executioner2, firstStrike, gymnastic, shadowed }, ["Brotherhood", "Seconding"]);
            mortificatorTrooperUnique.AddWeapon([avalanceHG, mortisBlade, smokes]);
            Unit mortificatorLeader = new("Mishima", "Mortificator", ["Leader", "Seconding"], 7, 2, 6, 14, 16, 6, -2, 20, 2, 13, 13, 30, "Mortificator", 1, new List<UnitSpecialAbility> { camouflage2, dodge, executioner2, firstStrike, gymnastic, shadowed, inspire, commandMT }, ["Brotherhood", "Seconding"]);
            mortificatorLeader.AddWeapon([silencedNemesisHG, mortisBlade, smokes]);

            Unit sacredWarriorTrooper = new("Brotherhood", "Sacred Warrior", ["Trooper"], 4, 0, 4, 12, 14, 5, -1, 23, 2, 13, 13, 30, null, 0, new List<UnitSpecialAbility> { faith1, fearless, duelist2}, ["Brotherhood"]);
            sacredWarriorTrooper.AddWeapon([avengerSword, r75RetributorCB]);
            Unit sacredWarriorSupport = new("Brotherhood", "Sacred Warrior", ["Support"], 4, -2, 4, 12, 14, 5, -1, 23, 2, 13, 13, 30, null, 0, new List<UnitSpecialAbility> { faith1, fearless, duelist2 }, ["Brotherhood"]);
            sacredWarriorSupport.AddWeapon([avengerSword, eruptorFT, eruptorLMG]);

            Unit furyEliteTrooper = new("Brotherhood", "Fury Elite Guard", ["Trooper"], 6, 0, 4, 13, 16, 5, -1, 23, 3, 14, 16, 30, null, 0, new List<UnitSpecialAbility> { faith1, fierceCharge }, ["Brotherhood"]);
            furyEliteTrooper.AddWeapon([delivererBattleBlade, punisherHandgun]);
            Unit furyEliteLeader = new("Brotherhood", "Fury Elite Guard", ["Leader"], 6, 3, 4, 12, 15, 5, -1, 23, 3, 14, 14, 30, "Any", 1, new List<UnitSpecialAbility> { faith1, fierceCharge, inspire, commandAB }, ["Brotherhood"]);
            furyEliteLeader.AddWeapon([delivererBattleBlade, punisherHandgun]);
            units.AddRange([mortificatorTrooper, mortificatorTrooperUnique, mortificatorLeader, sacredWarriorTrooper, sacredWarriorSupport, furyEliteTrooper, furyEliteLeader]);

            // Seed data for DLA Units
            Unit undeadLeagionnarie = new("Dark Legion - Algeroth", "Undead Legionnaire", ["Trooper", "Dark Cult"],
                2, 0, 4, 10, 10, 5, 1, 18, 2, 11, 8, 30,
                null, 0, 
                new List<UnitSpecialAbility> { group3, relentless, zombie }, ["Dark Legion - Algeroth", "Dark Cult"]);
            undeadLeagionnarie.AddWeapon([bladeBayonet, kratachAR]);

            Unit necromutantTrooper = new("Dark Legion - Algeroth", "Necromutant", ["Trooper", "Dark Cult"],
                4, 0, 4, 12, 12, 6, 0, 22, 2, 12, 11, 30,
                null, 0,
                new List<UnitSpecialAbility> { controller6, fearless }, ["Dark Legion - Algeroth", "Dark Cult"]);
            necromutantTrooper.AddWeapon([sectionerBayonet, belzarachAR]);
            Unit necromutantSupport = new("Dark Legion - Algeroth", "Necromutant", ["Support"],
               4, -1, 4, 12, 12, 6, 0, 22, 2, 12, 11, 30,
               null, 0,
               new List<UnitSpecialAbility> { fearless }, ["Dark Legion - Algeroth"]);
            necromutantSupport.AddWeapon([sectionerBayonet, tormentorFT, gasGrenade]);

            Unit necromutantLeader = new("Dark Legion - Algeroth", "Necromutant", ["Leader"],
                4, 1, 4, 11, 11, 6, 0, 22, 2, 12, 9, 30,
                null, 0,
                new List<UnitSpecialAbility> { controller6, fearless , commandUL, inspire}, ["Dark Legion - Algeroth"]);
            necromutantLeader.AddWeapon([sectionerBayonet, belzarachAR]);

            Unit centurion = new("Dark Legion - Algeroth", "Centurion", ["Leader"],
                6, 4, 5, 12, 14, 7, -1, 22, 3, 12, 13, 30,
                "Undead Legionnaire, Necromutant", 1,
                new List<UnitSpecialAbility> { controller9, commandULNM1, fierceCharge, inspire }, ["Dark Legion - Algeroth"]);
            centurion.AddWeapon([skalakSword, voricheHG, smokes]);

            Unit stalkers = new("Dark Legion - Algeroth", "Pretorian Stalker", ["Support"],
                10, -2, 5, 14, 14, 7, -1, 23, 3, 13, 14, 40,
                null, 0,
                new List<UnitSpecialAbility> { aggressive, camouflage1, packHunterPS,preciseSenses }, ["Dark Legion - Algeroth"]);
            stalkers.AddWeapon([sectionerBayonet, carcassGrenadeLauncher, incinerator, scytheOfSemai]);

            Unit razideHMG1 = new("Dark Legion - Algeroth", "Razide", ["Support"],
                12, -3, 4, 12, 14, 10, 2, 25, 4, 13, 14, 50,
                null, 0,
                new List<UnitSpecialAbility> { driven, firingStance, shakeItOff }, ["Dark Legion - Algeroth"]);
            razideHMG1.AddWeapon([nazgarothHMG]);
            Unit razideHMG2 = new("Dark Legion - Algeroth", "Razide", ["Support"],
                12, -3, 4, 12, 14, 10, 2, 25, 4, 13, 14, 50,
                null, 0,
                new List<UnitSpecialAbility> { driven, firingStance, shakeItOff }, ["Dark Legion - Algeroth"]);
            razideHMG2.AddWeapon([ashnazgarothHMG]);
            Unit razideHellblaster = new("Dark Legion - Algeroth", "Razide", ["Support"],
                12, -2, 4, 12, 14, 10, 2, 25, 4, 13, 14, 50,
                null, 0,
                new List<UnitSpecialAbility> { driven, firingStance, shakeItOff }, ["Dark Legion - Algeroth"]);
            razideHellblaster.AddWeapon([hellblasterLauncher]);

            Unit nepharite= new("Dark Legion - Algeroth", "Nepharite", ["Leader"],
                13, 2, 5, 12, 15, 10, 0, 24, 4, 15, 16, 50,
                "Undead Legionnaire, Necromutant", 4,
                new List<UnitSpecialAbility> {awareness, commandAA, controller12, inspire, nervesOfSteel, shakeItOff }, ["Dark Legion - Algeroth"]);
            nepharite.AddWeapon([azogar, devouringDarkness]);
            units.AddRange([undeadLeagionnarie, necromutantTrooper, necromutantSupport, necromutantLeader, centurion, stalkers, razideHMG1, razideHMG2, razideHellblaster, nepharite]);


            context.Units.AddRange(units);
            context.SaveChanges();
            // Seed data for Weapons

        }
    }
}
