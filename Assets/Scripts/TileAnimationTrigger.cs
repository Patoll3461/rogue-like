using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileAnimationTrigger : MonoBehaviour
{
    public Tilemap tilemap;           // Assign your Tilemap in the Inspector
    public Sprite[] animationFrames; // Assign the sprites for the animation
    public float animationSpeed = 0.00001f; // Time between frames

    private bool isAnimating = false;
    public bool extended = false;

    void Start() {
        StartCoroutine(Cycle());
    }

    public void TriggerAnimationForAllTiles()
    {
        if (!isAnimating)
        {
            StartCoroutine(AnimateAllTiles());
        }
    }

    private IEnumerator AnimateAllTiles()
    {
        isAnimating = true;

        // Get the bounds of the Tilemap
        BoundsInt bounds = tilemap.cellBounds;

        // Iterate over all the tiles in the bounds
        for (int frame = 0; frame < animationFrames.Length; frame++)
        {
            foreach (Vector3Int position in bounds.allPositionsWithin)
            {
                Tile tile = tilemap.GetTile<Tile>(position);
                if (tile != null)
                {
                    // Change the sprite of the tile
                    if (frame == 2) {
                        GetComponent<AudioSource>().Play();
                    }
                    if (frame == 4) {
                        extended = true;
                    }
                    if (frame == 7) {
                        extended = false;
                    }
                    tile.sprite = animationFrames[frame];
                    tilemap.RefreshTile(position);
                }
            }

            // Wait for the next frame
            yield return new WaitForSeconds(animationSpeed);
        }

        isAnimating = false;
    }

    private IEnumerator Cycle() {
        while (true) {
            TriggerAnimationForAllTiles();
            yield return new WaitForSeconds(3);
        }
    }
}
