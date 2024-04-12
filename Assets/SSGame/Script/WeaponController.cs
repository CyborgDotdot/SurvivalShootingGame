using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Weapon_Whip[] weaponPrefabs; // ��� ������ ���� ������ �迭
    private List<Weapon_Whip> equippedWeapons = new List<Weapon_Whip>(); // ���� ������ ���� ���

    // ���⸦ �����ϰ� �����ϴ� �޼���
    public void EquipWeapon(int weaponIndex, Vector3 position)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponPrefabs.Length)
        {
            Debug.LogError("WeaponController: �߸��� ���� �ε����Դϴ�.");
            return;
        }

        // �ش� �ε����� ���� �������� �ν��Ͻ�ȭ�Ͽ� �����մϴ�.
        Weapon_Whip newWeapon = Instantiate(weaponPrefabs[weaponIndex], position, Quaternion.identity, transform);
        equippedWeapons.Add(newWeapon);
    }

    // Ư�� ���⸦ �����ϴ� �޼���
    public void UnequipWeapon(Weapon_Whip weapon)
    {
        if (equippedWeapons.Contains(weapon))
        {
            equippedWeapons.Remove(weapon);
            Destroy(weapon.gameObject); // ���� ������Ʈ�� �ı��մϴ�.
        }
    }
}
