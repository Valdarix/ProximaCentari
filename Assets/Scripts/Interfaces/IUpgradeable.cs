using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeable
{
   int PowerLevel{ get; set; }
   void UpdatePowerLevel(int powerChange);

}
