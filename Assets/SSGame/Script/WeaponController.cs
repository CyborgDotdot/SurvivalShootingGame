using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Weapon_Whip[] weaponPrefabs; // 사용 가능한 무기 프리팹 배열
    private List<Weapon_Whip> equippedWeapons = new List<Weapon_Whip>(); // 현재 장착된 무기 목록

    // 무기를 생성하고 장착하는 메서드
    public void EquipWeapon(int weaponIndex, Vector3 position)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponPrefabs.Length)
        {
            Debug.LogError("WeaponController: 잘못된 무기 인덱스입니다.");
            return;
        }

        // 해당 인덱스의 무기 프리팹을 인스턴스화하여 장착합니다.
        Weapon_Whip newWeapon = Instantiate(weaponPrefabs[weaponIndex], position, Quaternion.identity, transform);
        equippedWeapons.Add(newWeapon);
    }

    // 특정 무기를 해제하는 메서드
    public void UnequipWeapon(Weapon_Whip weapon)
    {
        if (equippedWeapons.Contains(weapon))
        {
            equippedWeapons.Remove(weapon);
            Destroy(weapon.gameObject); // 무기 오브젝트를 파괴합니다.
        }
    }
}
